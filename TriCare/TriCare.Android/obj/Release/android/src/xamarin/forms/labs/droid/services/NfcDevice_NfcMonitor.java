package xamarin.forms.labs.droid.services;


public class NfcDevice_NfcMonitor
	extends xamarin.forms.labs.BroadcastMonitor
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onReceive:(Landroid/content/Context;Landroid/content/Intent;)V:GetOnReceive_Landroid_content_Context_Landroid_content_Intent_Handler\n" +
			"";
		mono.android.Runtime.register ("Xamarin.Forms.Labs.Droid.Services.NfcDevice/NfcMonitor, Xamarin.Forms.Labs.Droid, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null", NfcDevice_NfcMonitor.class, __md_methods);
	}


	public NfcDevice_NfcMonitor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == NfcDevice_NfcMonitor.class)
			mono.android.TypeManager.Activate ("Xamarin.Forms.Labs.Droid.Services.NfcDevice/NfcMonitor, Xamarin.Forms.Labs.Droid, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onReceive (android.content.Context p0, android.content.Intent p1)
	{
		n_onReceive (p0, p1);
	}

	private native void n_onReceive (android.content.Context p0, android.content.Intent p1);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
