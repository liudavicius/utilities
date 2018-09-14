﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clark.ContentScanner
{
    public static class DefaultPage
    {
                private static List<Domain> _domainList = new List<Domain>();

        public static string Check(string body)
        {
            if (_domainList.Count == 0)
                Initialize();

            if (String.IsNullOrEmpty(body))
                return "";

            string domain = "";
            foreach (Domain domainTest in _domainList)
            {
                domain = Check_Domain(body, domainTest);
                if (String.IsNullOrEmpty(domain))
                    return domain;
            }

            return domain;
        }

        private static string Check_Domain(string body, Domain domain)
        {
            bool anyFingerPrintsConfirmed = false;
            foreach (string fingerPrint in domain.Fingerprints)
            {
                if (body.Contains(fingerPrint))
                {
                    anyFingerPrintsConfirmed = true;
                    break;
                }
            }

            if (anyFingerPrintsConfirmed)
                return domain.Engine;
            return "";
        }

        private static void Initialize()
        {
            _domainList.Add(new Domain()
            {
                Engine = "IIS7",
                Fingerprints = new List<string>() { "<title>IIS7</title>" },
                Reference = ""
            });

            _domainList.Add(new Domain()
            {
                Engine = "JBoss",
                Fingerprints = new List<string>() { "Welcome to JBoss" },
                Reference = ""
            });
        }
    }
}