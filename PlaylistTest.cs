﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaPlayer;
using MediaPlayer.Library;
using MediaPlayer.Media;
using System.Collections.Generic;

namespace MediaPlayerTest
{
    [TestClass]
    public class PlaylistTest
    {
        private MyWindowsMediaPlayerV2 mp;
        private PlayList pl;

        [TestInitialize()]
        public void Init()
        {
            this.mp = new MyWindowsMediaPlayerV2();
            var dc = mp.ReadDir("../../MyMusic/");
            this.pl = new PlayList("Toast", dc.List);
        }

        [TestMethod]
        public void Display()
        {
            foreach (var media in pl.Content)
            {
                Console.WriteLine(media);
            }
        }

        [TestMethod]
        public void SimpleCreation()
        {
            Assert.AreEqual(7, pl.Content.Count);
            Assert.AreEqual("Toast", pl.Name);

            pl.Name = "ReToast";

            Assert.AreEqual("ReToast", pl.Name);
        }

        [TestMethod]
        public void RemoveMedia()
        {
            pl.Remove(pl.Content[0]);

            Assert.AreEqual(6, pl.Content.Count);
        }

        [TestMethod]
        public void AddMedia()
        {
            var med = pl.Content[0];

            pl.Add(med);

            Assert.AreEqual(7, pl.Content.Count);

            pl.Remove(med);
            pl.Add(med);

            Assert.AreEqual(7, pl.Content.Count);
        }

        [TestMethod]
        public void Filters()
        {
            var lst = pl.FiltersBy<Audio>(new Dictionary<string, string>
            {
                {"Artist", "Wild"}
            });

            Assert.AreEqual(3, lst.Count);

            lst = pl.FiltersBy<Audio>(new Dictionary<string, string>
            {
                {"TrackName", "u"}
            });

            Assert.AreEqual(3, lst.Count);

            lst = pl.FiltersBy<Audio>(new Dictionary<string, string>
            {
                {"TrackName", "u"},
                {"Album", "Gone"}
            });

            Assert.AreEqual(2, lst.Count);
        }
    }
}