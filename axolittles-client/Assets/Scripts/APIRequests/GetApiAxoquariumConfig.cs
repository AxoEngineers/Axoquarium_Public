using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GetApiAxoquariumConfig : Mingleton<GetApiAxoquariumConfig>
{
    private string url;
    
    private void Start()
    {
       url = "https://axo-backend-pvj2l.ondigitalocean.app/get_axoq_user_config?address="+MetamaskAuth.Instance.Wallet.eth_address;
        
    }

    public IEnumerator SendRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get(this.url);

        yield return request.SendWebRequest();

        
        
        AxoquariumConfig response = JsonUtility.FromJson<AxoquariumConfig>(request.downloadHandler.text);
       
        
        AquariumManager._Instance.LoadChanges(response.axoqUserConfig.axoItems);
      
    } 
   

   
}
