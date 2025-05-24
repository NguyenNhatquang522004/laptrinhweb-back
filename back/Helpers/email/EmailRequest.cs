﻿namespace backapi.Helpers.email
{
    public class EmailRequest
    {
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; } = true;
        public List<string> Attachments { get; set; } = new List<string>();
    }
}
