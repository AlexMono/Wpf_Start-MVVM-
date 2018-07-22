﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoLibrary;
using Wpf_Start_MVVM.ViewModel.Base;

namespace Wpf_Start_MVVM.ObjectValue
{
    public class OVDownload : BaseViewModel
    {        
        #region Members

        private string url;
        private string title;
        private string savePath;
        private bool isMp3;
        private bool isDownloading;

        #endregion

        #region Properties

        /// <summary>
        /// The url enter by user
        /// </summary>
        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
                OnPropertyChanged(nameof(Url));
            }
        }

        /// <summary>
        /// The title for the download
        /// </summary>
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        /// <summary>
        /// The path where the file is save after download
        /// </summary>
        public string SavePath
        {
            get
            {
                return savePath;
            }
            set
            {
                savePath = value;
                OnPropertyChanged(nameof(SavePath));
            }
        }

        /// <summary>
        /// If the file will be mp3 or mp4
        /// </summary>
        public bool IsMp3
        {
            get
            {
                return isMp3;
            }
            set
            {
                isMp3 = value;
                OnPropertyChanged(nameof(IsMp3));
            }
        }

        /// <summary>
        /// If the file is downloading
        /// </summary>
        public bool IsDownloading
        {
            get
            {
                return isDownloading;
            }
            set
            {
                isDownloading = value;
                OnPropertyChanged(nameof(IsDownloading));
            }
        }


        #endregion

        #region Constructor

        public OVDownload()
        {
            Initialize();
        }

        #endregion

        #region Public Methods

        public void Initialize()
        {
            IsDownloading = true;
        }

        public void Start()
        {
            var youTube = YouTube.Default; // starting point for YouTube actions
            var video = youTube.GetVideo(Url); // gets a Video object with info about the video            
            File.WriteAllBytes(SavePath + Title + ".mp3", video.GetBytes());
            IsDownloading = false;
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
