using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ClinicalStudy.Utils
{
    public sealed class LocalInfo
    {
        private static volatile LocalInfo instance;
        private static object syncRoot = new Object();

#if LCLSQL
        private String sqlConnectionString;

        public String SqlConnectionString
        {
            get { return sqlConnectionString; }
        }
#endif


        private String machineName;

        public String MachineName {
            get { return machineName; }
        }

        readonly private string _clinicalTrialInputData;

        public string ClinicalTrialInputData
        {
            get { return _clinicalTrialInputData; } 
        }

        readonly private string _clinicalTrialInputDataTutorial;

        public string ClinicalTrialInputDataTutorial
        {
            get { return _clinicalTrialInputDataTutorial; }
        }

#if LCLSQL
        private readonly string _dbLogIn;

        public  string DBLogIn
        {
            get
            {
                return _dbLogIn;
            }
        }
        
#endif
        private LocalInfo()
        {
            machineName = System.Environment.MachineName;
#if LCLSQL
            sqlConnectionString = ConfigurationManager.ConnectionStrings["sqlexp"+machineName].ConnectionString;
#endif
            _clinicalTrialInputData = "ClinicalTrialInputData_" + machineName;
            _clinicalTrialInputDataTutorial = "ClinicalTrialInputDataTutorial_" + machineName;


        }
        public static LocalInfo Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new LocalInfo();
                    }
                }

                return instance;
            }
        }


    }
}
