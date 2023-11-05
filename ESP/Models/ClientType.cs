﻿namespace ESP.Models
{
    public class ClientType
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public List<SubjectType> SubjectTypes = new List<SubjectType>();

        public List<CheckBlock> CheckBlocks { get; set; } = new List<CheckBlock>();

        public List<Process> Processes { get; set; } = new List<Process>();
     }
}
