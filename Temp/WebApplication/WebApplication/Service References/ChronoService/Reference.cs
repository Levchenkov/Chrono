﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication.ChronoService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ChronoMode", Namespace="http://schemas.datacontract.org/2004/07/StateWcfService")]
    public enum ChronoMode : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Read = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Write = 1,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="StateTimestamp", Namespace="http://schemas.datacontract.org/2004/07/StateWcfService")]
    [System.SerializableAttribute()]
    public partial class StateTimestamp : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TimestampField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Timestamp {
            get {
                return this.TimestampField;
            }
            set {
                if ((object.ReferenceEquals(this.TimestampField, value) != true)) {
                    this.TimestampField = value;
                    this.RaisePropertyChanged("Timestamp");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="State", Namespace="http://schemas.datacontract.org/2004/07/StateWcfService")]
    [System.SerializableAttribute()]
    public partial class State : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string KeyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WebApplication.ChronoService.StateTimestamp StateTimestampField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Key {
            get {
                return this.KeyField;
            }
            set {
                if ((object.ReferenceEquals(this.KeyField, value) != true)) {
                    this.KeyField = value;
                    this.RaisePropertyChanged("Key");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WebApplication.ChronoService.StateTimestamp StateTimestamp {
            get {
                return this.StateTimestampField;
            }
            set {
                if ((object.ReferenceEquals(this.StateTimestampField, value) != true)) {
                    this.StateTimestampField = value;
                    this.RaisePropertyChanged("StateTimestamp");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Value {
            get {
                return this.ValueField;
            }
            set {
                if ((object.ReferenceEquals(this.ValueField, value) != true)) {
                    this.ValueField = value;
                    this.RaisePropertyChanged("Value");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ChronoService.IStateService")]
    public interface IStateService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStateService/GetMode", ReplyAction="http://tempuri.org/IStateService/GetModeResponse")]
        WebApplication.ChronoService.ChronoMode GetMode();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStateService/GetMode", ReplyAction="http://tempuri.org/IStateService/GetModeResponse")]
        System.Threading.Tasks.Task<WebApplication.ChronoService.ChronoMode> GetModeAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStateService/EnableReadMode", ReplyAction="http://tempuri.org/IStateService/EnableReadModeResponse")]
        void EnableReadMode();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStateService/EnableReadMode", ReplyAction="http://tempuri.org/IStateService/EnableReadModeResponse")]
        System.Threading.Tasks.Task EnableReadModeAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStateService/EnableWriteMode", ReplyAction="http://tempuri.org/IStateService/EnableWriteModeResponse")]
        void EnableWriteMode();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStateService/EnableWriteMode", ReplyAction="http://tempuri.org/IStateService/EnableWriteModeResponse")]
        System.Threading.Tasks.Task EnableWriteModeAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStateService/Get", ReplyAction="http://tempuri.org/IStateService/GetResponse")]
        WebApplication.ChronoService.State Get(WebApplication.ChronoService.StateTimestamp timestamp, string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStateService/Get", ReplyAction="http://tempuri.org/IStateService/GetResponse")]
        System.Threading.Tasks.Task<WebApplication.ChronoService.State> GetAsync(WebApplication.ChronoService.StateTimestamp timestamp, string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStateService/Save", ReplyAction="http://tempuri.org/IStateService/SaveResponse")]
        void Save(WebApplication.ChronoService.State state);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStateService/Save", ReplyAction="http://tempuri.org/IStateService/SaveResponse")]
        System.Threading.Tasks.Task SaveAsync(WebApplication.ChronoService.State state);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStateService/Prev", ReplyAction="http://tempuri.org/IStateService/PrevResponse")]
        WebApplication.ChronoService.StateTimestamp Prev();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStateService/Prev", ReplyAction="http://tempuri.org/IStateService/PrevResponse")]
        System.Threading.Tasks.Task<WebApplication.ChronoService.StateTimestamp> PrevAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStateService/Next", ReplyAction="http://tempuri.org/IStateService/NextResponse")]
        WebApplication.ChronoService.StateTimestamp Next();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStateService/Next", ReplyAction="http://tempuri.org/IStateService/NextResponse")]
        System.Threading.Tasks.Task<WebApplication.ChronoService.StateTimestamp> NextAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStateService/Last", ReplyAction="http://tempuri.org/IStateService/LastResponse")]
        WebApplication.ChronoService.StateTimestamp Last();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStateService/Last", ReplyAction="http://tempuri.org/IStateService/LastResponse")]
        System.Threading.Tasks.Task<WebApplication.ChronoService.StateTimestamp> LastAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStateService/First", ReplyAction="http://tempuri.org/IStateService/FirstResponse")]
        WebApplication.ChronoService.StateTimestamp First();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStateService/First", ReplyAction="http://tempuri.org/IStateService/FirstResponse")]
        System.Threading.Tasks.Task<WebApplication.ChronoService.StateTimestamp> FirstAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStateService/CreateTimestamp", ReplyAction="http://tempuri.org/IStateService/CreateTimestampResponse")]
        WebApplication.ChronoService.StateTimestamp CreateTimestamp();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStateService/CreateTimestamp", ReplyAction="http://tempuri.org/IStateService/CreateTimestampResponse")]
        System.Threading.Tasks.Task<WebApplication.ChronoService.StateTimestamp> CreateTimestampAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IStateServiceChannel : WebApplication.ChronoService.IStateService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StateServiceClient : System.ServiceModel.ClientBase<WebApplication.ChronoService.IStateService>, WebApplication.ChronoService.IStateService {
        
        public StateServiceClient() {
        }
        
        public StateServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public StateServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StateServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StateServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public WebApplication.ChronoService.ChronoMode GetMode() {
            return base.Channel.GetMode();
        }
        
        public System.Threading.Tasks.Task<WebApplication.ChronoService.ChronoMode> GetModeAsync() {
            return base.Channel.GetModeAsync();
        }
        
        public void EnableReadMode() {
            base.Channel.EnableReadMode();
        }
        
        public System.Threading.Tasks.Task EnableReadModeAsync() {
            return base.Channel.EnableReadModeAsync();
        }
        
        public void EnableWriteMode() {
            base.Channel.EnableWriteMode();
        }
        
        public System.Threading.Tasks.Task EnableWriteModeAsync() {
            return base.Channel.EnableWriteModeAsync();
        }
        
        public WebApplication.ChronoService.State Get(WebApplication.ChronoService.StateTimestamp timestamp, string key) {
            return base.Channel.Get(timestamp, key);
        }
        
        public System.Threading.Tasks.Task<WebApplication.ChronoService.State> GetAsync(WebApplication.ChronoService.StateTimestamp timestamp, string key) {
            return base.Channel.GetAsync(timestamp, key);
        }
        
        public void Save(WebApplication.ChronoService.State state) {
            base.Channel.Save(state);
        }
        
        public System.Threading.Tasks.Task SaveAsync(WebApplication.ChronoService.State state) {
            return base.Channel.SaveAsync(state);
        }
        
        public WebApplication.ChronoService.StateTimestamp Prev() {
            return base.Channel.Prev();
        }
        
        public System.Threading.Tasks.Task<WebApplication.ChronoService.StateTimestamp> PrevAsync() {
            return base.Channel.PrevAsync();
        }
        
        public WebApplication.ChronoService.StateTimestamp Next() {
            return base.Channel.Next();
        }
        
        public System.Threading.Tasks.Task<WebApplication.ChronoService.StateTimestamp> NextAsync() {
            return base.Channel.NextAsync();
        }
        
        public WebApplication.ChronoService.StateTimestamp Last() {
            return base.Channel.Last();
        }
        
        public System.Threading.Tasks.Task<WebApplication.ChronoService.StateTimestamp> LastAsync() {
            return base.Channel.LastAsync();
        }
        
        public WebApplication.ChronoService.StateTimestamp First() {
            return base.Channel.First();
        }
        
        public System.Threading.Tasks.Task<WebApplication.ChronoService.StateTimestamp> FirstAsync() {
            return base.Channel.FirstAsync();
        }
        
        public WebApplication.ChronoService.StateTimestamp CreateTimestamp() {
            return base.Channel.CreateTimestamp();
        }
        
        public System.Threading.Tasks.Task<WebApplication.ChronoService.StateTimestamp> CreateTimestampAsync() {
            return base.Channel.CreateTimestampAsync();
        }
    }
}
