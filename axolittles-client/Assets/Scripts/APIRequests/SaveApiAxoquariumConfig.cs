using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class SaveApiAxoquariumConfig : Mingleton<SaveApiAxoquariumConfig>
{
    private string url;
   
   
    private void Start()
    {
        url = "https://axo-backend-pvj2l.ondigitalocean.app/save_axoq_user_config?address="+MetamaskAuth.Instance.Wallet.eth_address;
       
    }

    public IEnumerator SendRequest(AxoItems[] array)
    {
        WWWForm formData = new WWWForm();


       
       
               
        string json = ToJson(array, true);
       // Debug.Log(json);
        

        UnityWebRequest request = UnityWebRequest.Post(this.url, formData);
        byte[] postBytes = Encoding.UTF8.GetBytes(json);
        UploadHandler uploadHandler = new UploadHandlerRaw(postBytes);
        request.uploadHandler = uploadHandler;
        request.SetRequestHeader("Content-type", "application/json; charset=UTF-8");
        yield return request.SendWebRequest();
        //axoItems postFromServer = JsonUtility.FromJson<axoItems>(request.downloadHandler.text);
        if(request.isDone)
            Debug.Log("Request Done");
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Success");
        }
                
    }
    public  string ToJson(AxoItems[] array, bool prettyPrint)
    {
        APIUserConfig axoqUserConfig = new APIUserConfig();
        axoqUserConfig.axoItems=array;
        
        AxoquariumConfig axoquariumConfig = new AxoquariumConfig();
        
        axoquariumConfig.axoqUserConfig = axoqUserConfig;
        return JsonUtility.ToJson(axoquariumConfig, prettyPrint);
    }
 
}
