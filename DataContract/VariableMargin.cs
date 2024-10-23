using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace knowledgeBase.DataContract
{

    public class FindTopResponse
    {
        public List<VariableMargin> Margins { get; set; }
        public Intent Intent { get; set; }
    }
    public class VariableMargin
    {
        public NonBasisType NonBasisType { get; set; }

        public string VariableName { get; set; }

        public double Margin { get; set; }

        public double LowBound { get; set; }

        public double HighBound { get; set; }
    }
}