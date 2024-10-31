using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIQuestionAnswer.DataContract
{
    public enum Category
    {
        None = 0,
        FindTopMargin,
        AdjustMargin
    }
    public enum NonBasisType
    {
        All = 0,
        Purchase,
        Sales,
        Capacity,
        ProcLimit,
        Operation
    }
 
    public class Intent
    {
        public string Question { get; set; }
        public Category Category { get; set; }
        public float ConfidenceScore { get; set; } = 100;
        public int? TopNumber { get; set; }
        public string ModelName { get; set; }
        public string CaseName { get; set; }
        public NonBasisType NonBasisType { get; set; }

    }
}