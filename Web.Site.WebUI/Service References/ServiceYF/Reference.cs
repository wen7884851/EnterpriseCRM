namespace Web.Site.WebUI.ServiceYF {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceYF.CheckYFDataSoap")]
    public interface CheckYFDataSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/YFPersonCheck", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string YFPersonCheck(string RealName, string IDCard);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/YFPersonCheck", ReplyAction="*")]
        System.Threading.Tasks.Task<string> YFPersonCheckAsync(string RealName, string IDCard);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/YFPerson", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string YFPerson(string RealName, string IDCard);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/YFPerson", ReplyAction="*")]
        System.Threading.Tasks.Task<string> YFPersonAsync(string RealName, string IDCard);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CheckYFDataSoapChannel : Web.Site.WebUI.ServiceYF.CheckYFDataSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CheckYFDataSoapClient : System.ServiceModel.ClientBase<Web.Site.WebUI.ServiceYF.CheckYFDataSoap>, Web.Site.WebUI.ServiceYF.CheckYFDataSoap {
        
        public CheckYFDataSoapClient() {
        }
        
        public CheckYFDataSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CheckYFDataSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CheckYFDataSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CheckYFDataSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string YFPersonCheck(string RealName, string IDCard) {
            return base.Channel.YFPersonCheck(RealName, IDCard);
        }
        
        public System.Threading.Tasks.Task<string> YFPersonCheckAsync(string RealName, string IDCard) {
            return base.Channel.YFPersonCheckAsync(RealName, IDCard);
        }
        
        public string YFPerson(string RealName, string IDCard) {
            return base.Channel.YFPerson(RealName, IDCard);
        }
        
        public System.Threading.Tasks.Task<string> YFPersonAsync(string RealName, string IDCard) {
            return base.Channel.YFPersonAsync(RealName, IDCard);
        }
    }
}
