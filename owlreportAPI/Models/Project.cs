﻿using MessagePack;

namespace OwlreportAPI.Models
{
    public class Project
    {
        
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
    }
}
