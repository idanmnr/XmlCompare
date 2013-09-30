using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.XmlDiffPatch
{
    /// <summary>
    /// this class represents an element we want to ignore in the comparison
    /// an IgonreElement has an xpath string representing the xml element we want to ignore
    /// optional: an IgnoreElement can have conditions that need to be met in order to be ignored.
    /// </summary>
    public class IgnoreElement
    {
        public class XmlNamespace
        {
            public string Prefix { get; set; }
            public string Value { get; set; }
        }

        // the xpath to the element we want to ignore.
        // this element will be commented out from the xml file
        public string ElementXpath { get; set; }

        // the conditions to be met are also xpath strings that can refer to any other element in the xml file
        // if mutiple conditions are defined - all have to be met (AND not an OR)
        public IList<string> ElementConditions { get; set; }

        public IList<IgnoreElement.XmlNamespace> ElementNamespaces { get; set; }

        public IgnoreElement(string elementXpath)
        {
            ElementXpath = elementXpath;
            ElementConditions = new List<string>();
            ElementNamespaces = new List<XmlNamespace>();
        }

        public IgnoreElement(string elementXpath, string condition)
        {
            ElementXpath = elementXpath;
            ElementConditions = new List<string>();
            ElementConditions.Add(condition);
        }
    }
}
