﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HPIMS_Console_API.ServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/StatusOfServer", ReplyAction="http://tempuri.org/IService/StatusOfServerResponse")]
        string StatusOfServer(string serverName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/StatusOfServer", ReplyAction="http://tempuri.org/IService/StatusOfServerResponse")]
        System.Threading.Tasks.Task<string> StatusOfServerAsync(string serverName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/WhoIsLoggedIn", ReplyAction="http://tempuri.org/IService/WhoIsLoggedInResponse")]
        string WhoIsLoggedIn(string strComputer, string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/WhoIsLoggedIn", ReplyAction="http://tempuri.org/IService/WhoIsLoggedInResponse")]
        System.Threading.Tasks.Task<string> WhoIsLoggedInAsync(string strComputer, string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/DriveStorage", ReplyAction="http://tempuri.org/IService/DriveStorageResponse")]
        string[] DriveStorage(string strComputer, string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/DriveStorage", ReplyAction="http://tempuri.org/IService/DriveStorageResponse")]
        System.Threading.Tasks.Task<string[]> DriveStorageAsync(string strComputer, string username, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : HPIMS_Console_API.ServiceReference.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<HPIMS_Console_API.ServiceReference.IService>, HPIMS_Console_API.ServiceReference.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string StatusOfServer(string serverName) {
            return base.Channel.StatusOfServer(serverName);
        }
        
        public System.Threading.Tasks.Task<string> StatusOfServerAsync(string serverName) {
            return base.Channel.StatusOfServerAsync(serverName);
        }
        
        public string WhoIsLoggedIn(string strComputer, string username, string password) {
            return base.Channel.WhoIsLoggedIn(strComputer, username, password);
        }
        
        public System.Threading.Tasks.Task<string> WhoIsLoggedInAsync(string strComputer, string username, string password) {
            return base.Channel.WhoIsLoggedInAsync(strComputer, username, password);
        }
        
        public string[] DriveStorage(string strComputer, string username, string password) {
            return base.Channel.DriveStorage(strComputer, username, password);
        }
        
        public System.Threading.Tasks.Task<string[]> DriveStorageAsync(string strComputer, string username, string password) {
            return base.Channel.DriveStorageAsync(strComputer, username, password);
        }
    }
}