﻿using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbUp.Sample.Models
{
    [Verb("destroy", HelpText = "Delete the database along with seed data")]
    public class DestroyOptions
    {
        [Option("connection-string", Required = true, HelpText = "Connection string of database to be deleted")]
        public string ConnectionString { get; set; }
        
        [Option("force-flag", Required = true, Default = true, HelpText = "You are attempting to delete the database ! pass --force to ensure the action")]
        public bool Force { get; set; }
    }
}
