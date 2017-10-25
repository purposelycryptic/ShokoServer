﻿using System;
using System.Xml;
using Shoko.Commons.Queue;
using Shoko.Models.Queue;
using Shoko.Models.Server;
using Shoko.Server.Providers.Azure;

namespace Shoko.Server.Commands
{
    public class CommandRequest_WebCacheDeleteXRefFileEpisode : CommandRequestImplementation, ICommandRequest
    {
        public string Hash { get; set; }
        public int EpisodeID { get; set; }

        public CommandRequestPriority DefaultPriority => CommandRequestPriority.Priority10;

        public QueueStateStruct PrettyDescription => new QueueStateStruct
        {
            queueState = QueueStateEnum.WebCacheDeleteXRefFileEpisode,
            extraParams = new[] {Hash, EpisodeID.ToString()}
        };

        public CommandRequest_WebCacheDeleteXRefFileEpisode()
        {
        }

        public CommandRequest_WebCacheDeleteXRefFileEpisode(string hash, int aniDBEpisodeID)
        {
            Hash = hash;
            EpisodeID = aniDBEpisodeID;
            CommandType = (int) CommandRequestType.WebCache_DeleteXRefFileEpisode;
            Priority = (int) DefaultPriority;

            GenerateCommandID();
        }

        public override void ProcessCommand()
        {
            try
            {
                AzureWebAPI.Delete_CrossRefFileEpisode(Hash);
            }
            catch (Exception ex)
            {
                logger.Error("Error processing CommandRequest_WebCacheDeleteXRefFileEpisode: {0}-{1} - {2}", Hash,
                    EpisodeID,
                    ex);
            }
        }

        public override void GenerateCommandID()
        {
            CommandID = $"CommandRequest_WebCacheDeleteXRefFileEpisode-{Hash}-{EpisodeID}";
        }

        public override bool LoadFromDBCommand(CommandRequest cq)
        {
            CommandID = cq.CommandID;
            CommandRequestID = cq.CommandRequestID;
            CommandType = cq.CommandType;
            Priority = cq.Priority;
            CommandDetails = cq.CommandDetails;
            DateTimeUpdated = cq.DateTimeUpdated;

            // read xml to get parameters
            if (CommandDetails.Trim().Length > 0)
            {
                XmlDocument docCreator = new XmlDocument();
                docCreator.LoadXml(CommandDetails);

                // populate the fields
                Hash = TryGetProperty(docCreator, "CommandRequest_WebCacheDeleteXRefFileEpisode", "Hash");
                EpisodeID =
                    int.Parse(TryGetProperty(docCreator, "CommandRequest_WebCacheDeleteXRefFileEpisode", "EpisodeID"));
            }

            return true;
        }

        public override CommandRequest ToDatabaseObject()
        {
            GenerateCommandID();

            CommandRequest cq = new CommandRequest
            {
                CommandID = CommandID,
                CommandType = CommandType,
                Priority = Priority,
                CommandDetails = ToXML(),
                DateTimeUpdated = DateTime.Now
            };
            return cq;
        }
    }
}