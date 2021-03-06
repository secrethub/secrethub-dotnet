//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace SecretHub {

public class Secret : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal Secret(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Secret obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~Secret() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          SecretHubXGOPINVOKE.delete_Secret(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public System.Guid SecretID {
    // properties of Secret and SecretVersion are read only

    get {
        System.Guid ret = System.Guid.Parse(SecretHubXGOPINVOKE.Secret_SecretID_get(swigCPtr));
        return ret;
    }

  }

  public System.Guid DirID {
    // properties of Secret and SecretVersion are read only

    get {
        System.Guid ret = System.Guid.Parse(SecretHubXGOPINVOKE.Secret_DirID_get(swigCPtr));
        return ret;
    }

  }

  public System.Guid RepoID {
    // properties of Secret and SecretVersion are read only

    get {
        System.Guid ret = System.Guid.Parse(SecretHubXGOPINVOKE.Secret_RepoID_get(swigCPtr));
        return ret;
    }

  }

  public string Name {
    // properties of Secret and SecretVersion are read only

    get {
      string ret = SecretHubXGOPINVOKE.Secret_Name_get(swigCPtr);
      return ret;
    } 
  }

  public string BlindName {
    // properties of Secret and SecretVersion are read only

    get {
      string ret = SecretHubXGOPINVOKE.Secret_BlindName_get(swigCPtr);
      return ret;
    } 
  }

  public int VersionCount {
    // properties of Secret and SecretVersion are read only

    get {
      int ret = SecretHubXGOPINVOKE.Secret_VersionCount_get(swigCPtr);
      return ret;
    } 
  }

  public int LatestVersion {
    // properties of Secret and SecretVersion are read only

    get {
      int ret = SecretHubXGOPINVOKE.Secret_LatestVersion_get(swigCPtr);
      return ret;
    } 
  }

  public string Status {
    // properties of Secret and SecretVersion are read only

    get {
      string ret = SecretHubXGOPINVOKE.Secret_Status_get(swigCPtr);
      return ret;
    } 
  }

  public System.DateTime CreatedAt {
    // properties of Secret and SecretVersion are read only

    get {
        System.DateTime ret = System.DateTimeOffset.FromUnixTimeSeconds(SecretHubXGOPINVOKE.Secret_CreatedAt_get(swigCPtr)).UtcDateTime;
        return ret;
    }

  }

}

}
