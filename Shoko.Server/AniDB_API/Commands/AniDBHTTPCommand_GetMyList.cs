﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Shoko.Server;

namespace AniDBAPI.Commands
{
    public class AniDBHTTPCommand_GetMyList : AniDBHTTPCommand, IAniDBHTTPCommand
    {
        private List<Raw_AniDB_MyListFile> myListItems = new List<Raw_AniDB_MyListFile>();

        public List<Raw_AniDB_MyListFile> MyListItems
        {
            get { return myListItems; }
            set { myListItems = value; }
        }

        private string username = string.Empty;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private string password = string.Empty;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string GetKey()
        {
            return "AniDBHTTPCommand_GetMyList";
        }

        public virtual enHelperActivityType GetStartEventType()
        {
            return enHelperActivityType.GettingMyListHTTP;
        }

        private void WriteAnimeMyListToFile(string xml)
        {
            string filePath = ServerSettings.MyListDirectory;

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            //string fileName = string.Format("MyList_{0}_{1}.xml", DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HHmmss"));
            //string fileName = string.Format("MyList.xml");           Format function seems unnecessary here, remnant from previous implementation above?
            string fileName = "MyList.xml";
            string fileNameWithPath = Path.Combine(filePath, fileName);

            StreamWriter sw;
            sw = File.CreateText(fileNameWithPath);
            sw.Write(xml);
            sw.Close();
        }

        private XmlDocument LoadAnimeMyListFromFile()
        {
            string filePath = ServerSettings.MyListDirectory;

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            string fileName = string.Format("MyList.xml");
            string fileNameWithPath = Path.Combine(filePath, fileName);

            XmlDocument docAnime = null;
            if (File.Exists(fileNameWithPath))
            {
                StreamReader re = File.OpenText(fileNameWithPath);
                string rawXML = re.ReadToEnd();
                re.Close();

                docAnime = new XmlDocument();
                docAnime.LoadXml(rawXML);
            }

            return docAnime;
        }

        public virtual enHelperActivityType Process()
        {
            string xmlResult = AniDBHTTPHelper.GetMyListXMLFromAPI(username, password);
            XmlDocument docAnime = null;
            if (0 < xmlResult.Trim().Length)
            {
                WriteAnimeMyListToFile(xmlResult);
                docAnime = new XmlDocument();
                docAnime.LoadXml(xmlResult);
            }
            if (docAnime != null)
            {
                myListItems = AniDBHTTPHelper.ProcessMyList(docAnime);
                if (myListItems != null)
                	return enHelperActivityType.GotMyListHTTP;
            }

            return enHelperActivityType.NoSuchAnime;
        }

        public AniDBHTTPCommand_GetMyList()
        {
            commandType = enAniDBCommandType.GetMyListHTTP;
        }

        public void Init(string uname, string pword)
        {
            this.username = uname;
            this.password = pword;
            commandID = "MYLIST";
        }
    }
}