﻿using CrossCutting.Configuration.AppModels;

namespace CrossCutting.Configuration
{
    public class AppSettings
    {
        public static AppSettings Settings { get; set; }

        public JwtSettings Jwt { get; set; }

        public List<ServiceClientsSettings> ServiceClientsSettings { get; set; }
    }
}
