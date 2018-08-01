# Just me trying some things
This is just me trying out a few things, to see if I can narrow down some issues I've had without bothering the devs with them when I have no useful information on them, as well as to make some  minor adjustments based on personal preferences, and finally, to just to refresh my brain on coding as well as to maybe learn some new things. Probably not going to be a fork of much use to anyone else, sorry.

## Issues to look into
- [ ] Watched States and Votes from AniDB MyList not reflected in Shoko.
- [ ] MyList.xml not being properly built or updated - always solely composed of `<error code="310">illegal input or access denied</error>` - guessing related to previous point?
- [ ] ShokoMetadata for Plex pulls Series Poster Images for Episode Thumbnails - check if issue is server-side.
- [ ] ShokoMetadata for Plex sometimes pulls Series Poster and FanArt Images from entirely unrelated series on TheTVDB.com and adding in highest priority position, which are not present in Shoko itself - should be pulling images from Shoko itself, making this very strange; check server-side for potential cause.

## Feature Changes to Do
- [ ] Clean up Series Summary Text, removing AniDB internal links, "* Based on [...]", notes on early premier showings of episodes, and Summary Source citations (See [here](https://github.com/purposelycryptic/ShokoMetadata.bundle/commit/2ea41929990ca443ba720b4178791d58a7c9dde4) for implementation of same in ShokoMetadata for Plex.
- [ ] Clean up Episode Summary Text pulled from AniDB, removing Summary Source citations
- [ ] Switch Series Ratings used from AniDB Weighted Average to AniDB Plain Average (Plain Average is stored in __<ratings><temporary>__ in AniDB series XML file - Weighted Average is always stored in __<ratings><permanent>__. The two are just labeled the way they are due to being reused after the Weighted Average system was introduced).
- [ ] Possibly implement a toggle for switching between the two values in previous point.


# Original README

# Shoko Server
Shoko server is the core component of the Shoko suite and with it's client-server architecture which allows any program or plugin to access Shoko. You'll have access to your entire collection locally and over the internet with no additional work outside the initial configuration required.

# What Is Shoko?
Shoko is an anime cataloging program designed to automate the cataloging of your collection regardless of the size and amount of files you have. Unlike other anime cataloging programs which make you manually add your series or link the files to them, Shoko removes the tedious, time consuming and boring task of having to manually add every file and manually input the file information. You have better things to do with your time like actually watching the series in your collection so let Shoko handle all the heavy lifting.

# Learn More About Shoko
http://shokoanime.com/

# Learn More About Shoko Server
http://shokoanime.com/shoko-server/
