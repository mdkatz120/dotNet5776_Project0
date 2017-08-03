// Simon Moyal 1177707
//David Katz 065970394
using System;

namespace BE
{
    /// <summary>
    /// class branch 
    /// </summary>
    public class Branch
    {
        /// <summary>
        /// number telephone of the branch
        /// </summary>
        private long branchNumberTelephone;
        public long BranchNumberTelephone
        {
            set
            {
                // verification
                string tmp = value.ToString();
                if (tmp.Length < 8 || tmp.Length > 10)
                    throw new Exception("Incorrect format");
                branchNumberTelephone = value;
            }
            get
            {
                return branchNumberTelephone;
            }
        }
        /// <summary>
        /// Id of the branch
        /// </summary>
        public int BranchNumber { set; get; }
        /// <summary>
        /// number of worker in the sniff
        /// </summary>
        public int NumberOfWorker { set; get; }
        /// <summary>
        /// number of delivery man available in the sniff
        /// </summary>
        public int NumberDeliveryManAvailabe { set; get; }
        /// <summary>
        /// Name of the branch
        /// </summary>
        public string BranchName { set; get; }
        /// <summary>
        /// adress of the branch
        /// </summary>
        public string BranchAddress { set; get; }
        /// <summary>
        /// name of in charge of the branch
        /// </summary>
        public string NameIncharge { set; get; }
        /// <summary>
        /// Cashrout level of the brancg
        /// </summary>
        public CasheRoutLevel CashroutLevel { set; get; }
        /// <summary>
        /// Override ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Branch: {0}, CashRout: {1}, ID: {2}", BranchName, CashroutLevel, BranchNumber);
        }
    }
}

