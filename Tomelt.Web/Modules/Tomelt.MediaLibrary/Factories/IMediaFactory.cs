﻿using System.IO;
using Tomelt.MediaLibrary.Models;

namespace Tomelt.MediaLibrary.Factories {

    public interface IMediaFactory {
        MediaPart CreateMedia(Stream stream, string path, string mimeType, string contentType);
    }

    public class NullMediaFactory : IMediaFactory {
        public static IMediaFactory Instance {
            get { return new NullMediaFactory(); }
        }

        public MediaPart CreateMedia(Stream stream, string path, string mimeType, string contentType) {
            return null;
        }
    }
}