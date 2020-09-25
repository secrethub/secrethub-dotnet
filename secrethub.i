%module SecretHubXGO

// Map the time type to System.DateTime.
%apply long long { time };
%typemap(cstype) time "System.DateTime"
%typemap(csvarout, excode=SWIGEXCODE) time %{
    get {
        System.DateTime ret = System.DateTimeOffset.FromUnixTimeSeconds($imcall).UtcDateTime;$excode
        return ret;
    }
%}
%typemap(csvarin, excode=SWIGEXCODE) time %{
    // time is read only
%}

// Map the uuid type to System.Guid.
%apply char* { uuid };
%typemap(cstype) uuid "System.Guid"
%typemap(csvarout, excode=SWIGEXCODE) uuid %{
    get {
        System.Guid ret = System.Guid.Parse($imcall);$excode
        return ret;
    }
%}
%typemap(csvarin, excode=SWIGEXCODE) uuid %{
    // uuids are read only
%}

// Map return value of ResolveEnv to map[string]string.
%typemap(cstype) char* ResolveEnv "System.Collections.Generic.Dictionary<string,string>"
%typemap(csout, excode=SWIGEXCODE) char* ResolveEnv {
    var res = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.Dictionary<string, string>>($imcall);
    $excode
    return res;
}

%include secrethub-xgo/secrethub.i
