using System.Xml.Serialization;

namespace AsseccoBSWeb.Models.XmlDokument
{
    [XmlRoot(ElementName = "resource")]
    public class ResourcePath
    {
        [XmlAttribute(AttributeName = "path")]
        public string Path { get; set; }
    }

    [XmlRoot(ElementName = "resource")]
    public class Resource
    {
        [XmlElement(ElementName = "resource")]
        public ResourcePath ResourcePath { get; set; }
    }

    [XmlRoot(ElementName = "resource")]
    public class BaseResource
    {
        [XmlElement(ElementName = "resource")]
        public List<Resource> Resources { get; set; }
    }

    [XmlRoot(ElementName = "application", Namespace = "http://wadl.dev.java.net/2009/02")]
    public class XmlApplication
    {
        [XmlElement(ElementName = "resources")]
        public BaseResource BaseResource { get; set; }
    }
}
