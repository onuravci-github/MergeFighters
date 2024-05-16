using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Merge : MonoBehaviour
{
    public static int[] merge_Collected;

    public Vector3 downPosition;
    public GameObject controlObject;
    public Item_Images oldItemImages;
    public bool mouseUp = false;
    public bool isMouseItem = false;

    private IEnumerator createCroutine;
    public Transform[] createPos;
    private float createTime = 0.05f;
    private int createCount;


    private int controlItems = -1;
    private float oldTime;
    // Start is called before the first frame update
    void Start()
    {
        //Merge.merge_Collected = new int[25] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };

        createCroutine = CreateMergeItems(createTime);
        StartCoroutine(createCroutine);
        oldTime = 0;
    }

    // Update is called once per frame
    void Update()
    { 
        if(Time.time >= oldTime + 0.1f) {
            oldTime = Time.time;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);
            if(hit && hit.transform.gameObject.tag == "Merge_Img") {
                isMouseItem = false;
                controlObject = hit.collider.gameObject;
                if(controlObject.GetComponent<Merge_Images>().placeNumb < 0) {
                    controlObject.GetComponent<Merge_Images>().isActive = true;
                    downPosition = controlObject.transform.position;
                }
                else {
                    controlObject = null;
                }
            }
            else if(hit && hit.transform.gameObject.tag == "Item_Img") {
                if(hit.collider.gameObject.GetComponent<Item_Images>().interectObj != null) {
                    isMouseItem = true;
                    oldItemImages = hit.collider.gameObject.GetComponent<Item_Images>();
                    controlObject = oldItemImages.interectObj;
                    controlObject.GetComponent<Merge_Images>().colliderBox.enabled = true;
                    controlObject.GetComponent<Merge_Images>().GetComponent<Image>().raycastTarget = true;
                    if(controlObject.GetComponent<Merge_Images>().placeNumb < 0) {
                        controlObject.GetComponent<Merge_Images>().isActive = true;
                        downPosition = controlObject.transform.position;
                    }
                    else {
                        isMouseItem = false;
                        controlObject = null;
                    }
                }
            }
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0)) {
            if(isMouseItem) {
                Debug.Log("A");

                if(controlObject != null && controlObject.GetComponent<Merge_Images>().itemObject) {
                    Merge_Images ctrlMerge_Img1 = controlObject.GetComponent<Merge_Images>();
                    Debug.Log("B");
                    if(ctrlMerge_Img1.itemObject.GetComponent<Item_Images>().interectObj == null) {
                        Debug.Log("C");
                        Items.eguipItems[oldItemImages.itemAreaNumb] = null;
                        oldItemImages.interectObj = null;
                        ctrlMerge_Img1.itemObject.GetComponent<Item_Images>().interectObj = controlObject;
                        controlObject.transform.position = new Vector2(ctrlMerge_Img1.itemObject.transform.position.x, ctrlMerge_Img1.itemObject.transform.position.y);
                        Items.eguipItems[ctrlMerge_Img1.itemObject.GetComponent<Item_Images>().itemAreaNumb] = controlObject;
                        ctrlMerge_Img1.colliderBox.enabled = false;
                        ctrlMerge_Img1.GetComponent<Image>().raycastTarget = false;
                        ctrlMerge_Img1.posChangeObj = null;
                        controlObject.GetComponent<Merge_Images>().isActive = false;
                        ctrlMerge_Img1.posChangeObj = null;
                        ctrlMerge_Img1.stayObject = null;
                        ctrlMerge_Img1.itemObject = null;
                        controlObject = null;
                    }
                    else if(ctrlMerge_Img1.itemObject.GetComponent<Item_Images>().interectObj != null && ctrlMerge_Img1.itemObject.GetComponent<Item_Images>().interectObj != controlObject) {
                        Items.eguipItems[ctrlMerge_Img1.itemObject.GetComponent<Item_Images>().itemAreaNumb] = controlObject;
                        var tempObject = ctrlMerge_Img1.itemObject.GetComponent<Item_Images>().interectObj;
                        Items.eguipItems[oldItemImages.itemAreaNumb] = tempObject;

                        var tempPos = ctrlMerge_Img1.itemObject.GetComponent<Item_Images>().interectObj.transform.position;
                        ctrlMerge_Img1.itemObject.GetComponent<Item_Images>().interectObj.transform.position = new Vector2(downPosition.x, downPosition.y);
                        controlObject.transform.position = new Vector2(tempPos.x, tempPos.y);

                        ctrlMerge_Img1.itemObject.GetComponent<Item_Images>().interectObj = controlObject;
                        oldItemImages.interectObj = tempObject;

                        ctrlMerge_Img1.colliderBox.enabled = false;
                        ctrlMerge_Img1.GetComponent<Image>().raycastTarget = false;
                        controlObject.GetComponent<Merge_Images>().isActive = false;
                        ctrlMerge_Img1.posChangeObj = null;
                        ctrlMerge_Img1.stayObject = null;
                        ctrlMerge_Img1.itemObject = null;
                        controlObject = null;
                    }
                    else {
                        controlObject.transform.position = downPosition;
                        ctrlMerge_Img1.colliderBox.enabled = false;
                        ctrlMerge_Img1.GetComponent<Image>().raycastTarget = false;
                        controlObject.GetComponent<Merge_Images>().isActive = false;
                        ctrlMerge_Img1.posChangeObj = null;
                        ctrlMerge_Img1.stayObject = null;
                        ctrlMerge_Img1.itemObject = null;
                        controlObject = null;
                    }
                }
                else if(controlObject != null && !controlObject.GetComponent<Merge_Images>().itemObject) {
                    Debug.Log("C");

                    Merge_Images ctrlMerge_Img2 = controlObject.GetComponent<Merge_Images>();
                    if(ctrlMerge_Img2.posChangeObj != null && ctrlMerge_Img2.interactObj1) {

                        Items.eguipItems[oldItemImages.itemAreaNumb] = null;
                        oldItemImages.interectObj = null;
                        controlObject.transform.position = new Vector2(ctrlMerge_Img2.posChangeObj.transform.position.x, ctrlMerge_Img2.posChangeObj.transform.position.y);
                        

                        if((ctrlMerge_Img2.imageNumb + 1) % 3 != 0) {
                            Instantiate(Resources.Load<GameObject>("Merge_Images/Merge_Img_" + (ctrlMerge_Img2.imageNumb + 1).ToString()), ctrlMerge_Img2.interactObj1.transform.position, Quaternion.identity, transform);
                            ctrlMerge_Img2.isActive = false;
                            Destroy(ctrlMerge_Img2.interactObj1);
                            Destroy(controlObject);
                        }
                        controlObject.GetComponent<Merge_Images>().isActive = false;
                        ctrlMerge_Img2.posChangeObj = null;
                        ctrlMerge_Img2.stayObject = null;
                        ctrlMerge_Img2.itemObject = null;
                        controlObject = null;
                    }
                    else if(ctrlMerge_Img2.posChangeObj != null && ctrlMerge_Img2.posChangeObj.GetComponent<Merge_Images>()) {
                        Debug.Log("BBB");
                        var tempControlObjectPos = oldItemImages.interectObj.transform.position;
                        var tempPosChangeObj = ctrlMerge_Img2.posChangeObj;
                        var tempPosChangeObjectPos = tempPosChangeObj.transform.position;

                        Items.eguipItems[oldItemImages.itemAreaNumb] = ctrlMerge_Img2.posChangeObj;
                        oldItemImages.interectObj = ctrlMerge_Img2.posChangeObj;


                        tempPosChangeObj.GetComponent<Image>().raycastTarget = false;
                        tempPosChangeObj.GetComponent<Merge_Images>().colliderBox.enabled = false;
                        tempPosChangeObj.GetComponent<Merge_Images>().posChangeObj = null;

                        ctrlMerge_Img2.GetComponent<Merge_Images>().colliderBox.enabled = true;
                        ctrlMerge_Img2.GetComponent<Image>().raycastTarget = true;


                        controlObject.transform.position = new Vector2(tempPosChangeObjectPos.x, tempPosChangeObjectPos.y);
                        tempPosChangeObj.transform.position = downPosition;
                        controlObject.GetComponent<Merge_Images>().isActive = false;
                        ctrlMerge_Img2.posChangeObj = null;
                        ctrlMerge_Img2.stayObject = null;
                        ctrlMerge_Img2.itemObject = null;
                        controlObject = null;
                    }
                    else if(ctrlMerge_Img2.posChangeObj != null && ctrlMerge_Img2.posChangeObj.tag != "Item_Img") {
                        Debug.Log("D");
                        Items.eguipItems[oldItemImages.itemAreaNumb] = null;
                        oldItemImages.interectObj = null;
                        controlObject.transform.position = new Vector2(ctrlMerge_Img2.posChangeObj.transform.position.x, ctrlMerge_Img2.posChangeObj.transform.position.y);
                        controlObject.GetComponent<Merge_Images>().isActive = false;
                        ctrlMerge_Img2.colliderBox.enabled = true;
                        ctrlMerge_Img2.GetComponent<Image>().raycastTarget = true;
                        ctrlMerge_Img2.posChangeObj = null;
                        ctrlMerge_Img2.stayObject = null;
                        ctrlMerge_Img2.itemObject = null;
                        controlObject = null;
                    }
                    else if(ctrlMerge_Img2.posChangeObj != null && ctrlMerge_Img2.posChangeObj.tag == "Item_Img") {
                        ctrlMerge_Img2.colliderBox.enabled = false;
                        ctrlMerge_Img2.GetComponent<Image>().raycastTarget = false;
                        controlObject.transform.position = downPosition;
                        ctrlMerge_Img2.isActive = false;
                        ctrlMerge_Img2.posChangeObj = null;
                        ctrlMerge_Img2.stayObject = null;
                        ctrlMerge_Img2.itemObject = null;
                        controlObject = null;
                    }
                }
            }
            else if(controlObject != null && controlObject.GetComponent<Merge_Images>().itemObject) {
                Debug.Log("Merge Basýldý item objesi var ");

                Merge_Images ctrlMerge_Img = controlObject.GetComponent<Merge_Images>();
                if(ctrlMerge_Img.itemObject.GetComponent<Item_Images>().interectObj == null) {
                    Debug.Log("Merge Basýldý item objesinin interact objesi yok ");
                    bool control = false;
                    if(Items.eguipItems != null) {
                        for(int i = 0; i < Items.eguipItems.Length; i++) {
                            if(Items.eguipItems[i] != null) {
                                controlItems = Items.eguipItems[i].GetComponent<Merge_Images>().imageNumb / 3;
                                if(controlItems == controlObject.GetComponent<Merge_Images>().imageNumb / 3) {
                                    control = true;
                                    Debug.Log("Merge Basýldý item objesinin interact objesi yok ve ayný tarz takýlý");
                                }
                            }
                        }
                    }
                    if(!control) {
                        Debug.Log("Merge Basýldý item objesinin interact objesi yok ve ayný tarz takýlý deðil");
                        ctrlMerge_Img.itemObject.GetComponent<Item_Images>().interectObj = controlObject;
                        controlObject.transform.position = new Vector2(ctrlMerge_Img.itemObject.transform.position.x, ctrlMerge_Img.itemObject.transform.position.y);
                        Items.eguipItems[ctrlMerge_Img.itemObject.GetComponent<Item_Images>().itemAreaNumb] = controlObject;
                        ctrlMerge_Img.colliderBox.enabled = false;
                        ctrlMerge_Img.GetComponent<Image>().raycastTarget = false;
                        ctrlMerge_Img.posChangeObj = null;
                        ctrlMerge_Img.stayObject = null;
                        ctrlMerge_Img.itemObject = null;
                        controlObject.GetComponent<Merge_Images>().isActive = false;
                        controlObject = null;
                    }
                    else {
                        Debug.Log("Merge Basýldý item objesinin interact objesi yok ve ayný tarz takýlý");
                        controlObject.transform.position = downPosition;
                        controlObject.GetComponent<Merge_Images>().isActive = false;
                        controlObject = null;
                    }
                    /*item koyma yeri boþ sadece pozisyon deðiþtirilicek
                     *item images ýn interectine atama yapýlýcak items lara kaydettiricez*/
                }
                else if(ctrlMerge_Img.itemObject.GetComponent<Item_Images>().interectObj != null) {
                    Debug.Log("Merge Basýldý item objesinin interact objesi var");
                    bool control = false;
                    if(Items.eguipItems != null) {
                        for(int i = 0; i < Items.eguipItems.Length; i++) {
                            if(Items.eguipItems[i] != null) {
                                controlItems = Items.eguipItems[i].GetComponent<Merge_Images>().imageNumb / 3;
                                if(controlItems == controlObject.GetComponent<Merge_Images>().imageNumb / 3) {
                                    control = true;
                                    Debug.Log("Merge Basýldý item objesinin interact objesi var ve ayný tarz takýlý");
                                }
                            }
                        }
                        if(ctrlMerge_Img.itemObject.GetComponent<Item_Images>().interectObj.GetComponent<Merge_Images>().imageNumb  == ctrlMerge_Img.imageNumb) {
                            Debug.Log("Merge Basýldý item objesinin interact objesi var ve ayný tarz takýlý ve numaralarý ayný");
                            if((ctrlMerge_Img.imageNumb + 1) % 3 != 0) {
                                Debug.Log("Merge Basýldý item objesinin interact objesi var ve ayný tarz takýlý ve numaralarý ayný ve son aþamaada deðil");
                                var tempMergeImg = Instantiate(Resources.Load<GameObject>("Merge_Images/Merge_Img_" + (ctrlMerge_Img.imageNumb + 1).ToString()), ctrlMerge_Img.itemObject.GetComponent<Item_Images>().interectObj.transform.position, Quaternion.identity, transform);
                                //controlObject.transform.position = new Vector2(ctrlMerge_Img.posChangeObj.transform.position.x, ctrlMerge_Img.posChangeObj.transform.position.y);
                                ctrlMerge_Img.isActive = false;
                                Destroy(ctrlMerge_Img.itemObject.GetComponent<Item_Images>().interectObj);
                                Items.eguipItems[ctrlMerge_Img.itemObject.GetComponent<Item_Images>().itemAreaNumb] = tempMergeImg;
                                ctrlMerge_Img.itemObject.GetComponent<Item_Images>().interectObj = tempMergeImg;
                                tempMergeImg.GetComponent<Image>().raycastTarget = false;
                                tempMergeImg.GetComponent<Merge_Images>().colliderBox.enabled = false;
                                Destroy(controlObject);
                            }
                            controlObject.GetComponent<Merge_Images>().isActive = false;
                            controlObject.GetComponent<Merge_Images>().posChangeObj = null;
                            controlObject = null;
                        }
                        else if(ctrlMerge_Img.itemObject.GetComponent<Item_Images>().interectObj.GetComponent<Merge_Images>().imageNumb / 3 == ctrlMerge_Img.imageNumb / 3) {
                            Debug.Log("Merge Basýldý item objesinin interact objesi var ve ayný tarz takýlý ve numaralarý ayný deðil");


                            var tempPosChangeObj = ctrlMerge_Img.itemObject.GetComponent<Item_Images>().interectObj;
                            var tempPosChangeObjectPos = tempPosChangeObj.transform.position;

                            Items.eguipItems[ctrlMerge_Img.itemObject.GetComponent<Item_Images>().itemAreaNumb] = controlObject;
                            ctrlMerge_Img.itemObject.GetComponent<Item_Images>().interectObj = controlObject;


                            ctrlMerge_Img.GetComponent<Image>().raycastTarget = false;
                            ctrlMerge_Img.GetComponent<Merge_Images>().colliderBox.enabled = false;
                            ctrlMerge_Img.GetComponent<Merge_Images>().posChangeObj = null;

                            tempPosChangeObj.GetComponent<Merge_Images>().colliderBox.enabled = true;
                            tempPosChangeObj.GetComponent<Image>().raycastTarget = true;


                            controlObject.transform.position = new Vector2(tempPosChangeObjectPos.x, tempPosChangeObjectPos.y);
                            tempPosChangeObj.transform.position = downPosition;
                            controlObject.GetComponent<Merge_Images>().posChangeObj = null;
                            controlObject.GetComponent<Merge_Images>().isActive = false;
                            controlObject = null;
                        }
                        else if(!control && ctrlMerge_Img.itemObject.GetComponent<Item_Images>().interectObj.GetComponent<Merge_Images>().imageNumb / 3 != ctrlMerge_Img.imageNumb / 3) {
                            Debug.Log("Merge Basýldý item objesinin interact objesi var ve ayný tarz takýlý deðil ve numaralarý ayný deðil");

                            var tempPosChangeObj = ctrlMerge_Img.itemObject.GetComponent<Item_Images>().interectObj;
                            var tempPosChangeObjectPos = tempPosChangeObj.transform.position;

                            Items.eguipItems[ctrlMerge_Img.itemObject.GetComponent<Item_Images>().itemAreaNumb] = controlObject;
                            ctrlMerge_Img.itemObject.GetComponent<Item_Images>().interectObj = controlObject;


                            ctrlMerge_Img.GetComponent<Image>().raycastTarget = false;
                            ctrlMerge_Img.stayObject = null;
                            ctrlMerge_Img.posChangeObj = null;
                            ctrlMerge_Img.itemObject = null;
                            ctrlMerge_Img.GetComponent<Merge_Images>().colliderBox.enabled = false;
                            ctrlMerge_Img.GetComponent<Merge_Images>().posChangeObj = null;

                            tempPosChangeObj.GetComponent<Merge_Images>().colliderBox.enabled = true;
                            tempPosChangeObj.GetComponent<Image>().raycastTarget = true;
                            tempPosChangeObj.GetComponent<Merge_Images>().stayObject = null;
                            tempPosChangeObj.GetComponent<Merge_Images>().posChangeObj = null;
                            tempPosChangeObj.GetComponent<Merge_Images>().itemObject = null;


                            controlObject.transform.position = new Vector2(tempPosChangeObjectPos.x, tempPosChangeObjectPos.y);
                            controlObject.GetComponent<Merge_Images>().posChangeObj = null;
                            tempPosChangeObj.transform.position = downPosition;
                            controlObject.GetComponent<Merge_Images>().isActive = false;
                            controlObject = null;

                        }
                    }
                    if(!control && controlObject != null) {
                        Debug.Log("Merge Basýldý item objesinin interact objesi var ve ayný tarz takýlý deðil numarasý ayný deðil tarzý ayný deðil ve ayný tarz takýlý deðil numaralarý ayný deðil");

                        /*Debug.Log("FFF");
                         ctrlMerge_Img.itemObject.GetComponent<Item_Images>().interectObj = controlObject;
                         controlObject.transform.position = new Vector2(ctrlMerge_Img.itemObject.transform.position.x, ctrlMerge_Img.itemObject.transform.position.y);
                         Items.eguipItems[ctrlMerge_Img.itemObject.GetComponent<Item_Images>().itemAreaNumb] = controlObject;
                         ctrlMerge_Img.colliderBox.enabled = false;
                         ctrlMerge_Img.GetComponent<Image>().raycastTarget = false;*/
                        controlObject.transform.position = downPosition;
                        controlObject.GetComponent<Merge_Images>().posChangeObj = null;
                        controlObject.GetComponent<Merge_Images>().isActive = false;
                        controlObject = null;
                    }


                    if(controlObject) {
                        Debug.Log("Merge Basýldý item objesinin interact objesi var ve ayný tarz takýlý numarasý ayný deðil tarzý ayný deðil ve ayný tarz takýlý deðil numaralarý ayný deðil");

                        //Transform temp = ctrlMerge_Img.itemObject.GetComponent<Item_Images>().interectObj.transform;
                        //ctrlMerge_Img.itemObject.GetComponent<Item_Images>().interectObj = controlObject;
                        //controlObject.transform.position = new Vector2(temp.position.x, temp.position.y);
                        controlObject.GetComponent<Merge_Images>().stayObject = null;
                        controlObject.GetComponent<Merge_Images>().posChangeObj = null;
                        controlObject.GetComponent<Merge_Images>().itemObject = null;
                        controlObject.transform.position = downPosition;
                        controlObject.GetComponent<Merge_Images>().isActive = false;
                        controlObject = null;
                    }
                    //Items.eguipItems[]
                    /*üzerinde obje var ise burada iki objenin yerlerini deðiþtirmem gerekli */
                }

            }
            else if(controlObject != null && !controlObject.GetComponent<Merge_Images>().isItemPlace) {
                Debug.Log("HATA BURADA");

                Merge_Images ctrlMerge_Img = controlObject.GetComponent<Merge_Images>();
                if(ctrlMerge_Img.interactObj1 != null) {
                    Debug.Log("HATA BURADA interact obje 1 var");
                    if((ctrlMerge_Img.imageNumb + 1) % 3 != 0) {
                        Debug.Log("HATA BURADA interact obje 1 var ve son aþamada deðil");
                        Instantiate(Resources.Load<GameObject>("Merge_Images/Merge_Img_" + (ctrlMerge_Img.imageNumb + 1).ToString()), ctrlMerge_Img.interactObj1.transform.position, Quaternion.identity, transform);
                        ctrlMerge_Img.isActive = false;
                        Destroy(ctrlMerge_Img.interactObj1);
                        ctrlMerge_Img.posChangeObj = null;
                        ctrlMerge_Img.stayObject = null;
                        ctrlMerge_Img.itemObject = null;
                        ctrlMerge_Img = null;
                        //Destroy(ctrlMerge_Img);
                        Destroy(controlObject);
                    }
                    else {
                        Debug.Log("HATA BURADA interact obje 1 var ve son aþamada");
                        controlObject.transform.position = downPosition;
                        ctrlMerge_Img.isActive = false;
                        ctrlMerge_Img.interactObj1 = null;
                        ctrlMerge_Img.posChangeObj = null;
                        ctrlMerge_Img.stayObject = null;
                        controlObject = null;
                    }
                }
                else if(ctrlMerge_Img != null && ctrlMerge_Img.interactObj1 == null && ctrlMerge_Img.posChangeObj && ctrlMerge_Img.posChangeObj.tag == "Item_Img") {
                    Debug.Log("HATA BURADA interact obje yok poschange obje item yeri");
                    ctrlMerge_Img.isActive = false;
                    ctrlMerge_Img.interactObj1 = null;
                    ctrlMerge_Img.posChangeObj = null;
                    ctrlMerge_Img.posChangeObj = null;
                    ctrlMerge_Img.stayObject = null;
                    controlObject.transform.position = downPosition;
                    controlObject = null;
                }
                if(ctrlMerge_Img != null && ctrlMerge_Img.posChangeObj != null) {
                    Debug.Log("HATA BURADA interact obje yok poschange obje merge yerinde null deðil");
                    if(ctrlMerge_Img.posChangeObj.tag == "Merge_Pos") {
                        Debug.Log("HATA BURADA interact obje yok poschange obje merge yerinde null deðil pos change Merge_Pos etiketli");
                        if(ctrlMerge_Img.interactObj1 == null) {
                            Debug.Log("HATA BURADA interact obje yok poschange obje merge yerinde null deðil pos change Merge_Pos etiketli ama interact1 hala boþ");

                            controlObject.transform.position = new Vector2(ctrlMerge_Img.posChangeObj.transform.position.x, ctrlMerge_Img.posChangeObj.transform.position.y);
                            ctrlMerge_Img.posChangeObj = null;
                            ctrlMerge_Img.stayObject = null;
                            ctrlMerge_Img.itemObject = null;
                            controlObject = null;
                            if(ctrlMerge_Img.placeNumb >= 0) {
                                Items.eguipItems[ctrlMerge_Img.placeNumb] = null;
                            }
                            ctrlMerge_Img.placeNumb = -1;
                        }
                        else {
                            controlObject.transform.position = downPosition;
                        }
                        ctrlMerge_Img.isItemPlace = false;
                    }
                    else if(ctrlMerge_Img.posChangeObj.tag == "Merge_Img") {
                        Debug.Log("HATA BURADA interact obje yok poschange obje merge yerinde null deðil pos change Merge_Img etiketli");
                        if(ctrlMerge_Img.stayObject.tag == "Item_Img") {
                            ctrlMerge_Img.isActive = false;
                            ctrlMerge_Img.interactObj1 = null;
                            ctrlMerge_Img.posChangeObj = null;
                            ctrlMerge_Img.stayObject = null;
                            controlObject.transform.position = downPosition;
                            controlObject = null;
                        }
                        else if(ctrlMerge_Img.stayObject.tag == "Merge_Pos") {
                            Debug.Log("HATA BURADA interact obje yok poschange obje merge yerinde null deðil pos change Merge_Img etiketli stay obje merge_img etiketli");
                            controlObject.transform.position = ctrlMerge_Img.posChangeObj.transform.position;
                            ctrlMerge_Img.posChangeObj.transform.position = downPosition;
                            ctrlMerge_Img.isActive = false;
                            ctrlMerge_Img.interactObj1 = null;
                            ctrlMerge_Img.posChangeObj = null;
                            ctrlMerge_Img.stayObject = null;
                            controlObject = null;
                        }
                        else if(ctrlMerge_Img.stayObject.tag == "Merge_Img") {
                            Debug.Log("HATA BURADA interact obje yok poschange obje merge yerinde null deðil pos change Merge_Img etiketli stay obje merge_img etiketli");
                            controlObject.transform.position = ctrlMerge_Img.posChangeObj.transform.position;
                            ctrlMerge_Img.posChangeObj.transform.position = downPosition;
                            ctrlMerge_Img.isActive = false;
                            ctrlMerge_Img.interactObj1 = null;
                            ctrlMerge_Img.stayObject = null;
                            ctrlMerge_Img.posChangeObj = null;
                            controlObject = null;
                        }
                    }
                }
                else if(ctrlMerge_Img != null && controlObject != null) {
                    if(!ctrlMerge_Img.isItemPlace) {
                        controlObject.transform.position = downPosition;
                    }
                    else if(ctrlMerge_Img.isItemPlace) {
                    }
                }
                if(ctrlMerge_Img != null && controlObject != null) {
                    ctrlMerge_Img.isActive = false;
                    ctrlMerge_Img.interactObj1 = null;
                    ctrlMerge_Img.posChangeObj = null;
                    ctrlMerge_Img.stayObject = null;
                    controlObject.transform.position = downPosition;
                    controlObject = null;
                }

            }
            else if(controlObject != null && controlObject.GetComponent<Merge_Images>().isItemPlace && controlObject.GetComponent<Merge_Images>().posChangeObj != null) {
                if(controlObject.GetComponent<Merge_Images>().posChangeObj.tag == "Merge_Img") {
                    controlObject.transform.position = controlObject.GetComponent<Merge_Images>().posChangeObj.transform.position;
                    controlObject.GetComponent<Merge_Images>().posChangeObj.transform.position = downPosition;

                    if(controlObject.GetComponent<Merge_Images>().placeNumb >= 0) {
                        Items.eguipItems[controlObject.GetComponent<Merge_Images>().placeNumb] = controlObject.GetComponent<Merge_Images>().posChangeObj;
                        controlObject.GetComponent<Merge_Images>().posChangeObj.GetComponent<Merge_Images>().placeNumb = controlObject.GetComponent<Merge_Images>().placeNumb;
                        controlObject.GetComponent<Merge_Images>().placeNumb = -1;
                    }
                    else if(controlObject.GetComponent<Merge_Images>().posChangeObj.GetComponent<Merge_Images>().placeNumb >= 0) {
                        Items.eguipItems[controlObject.GetComponent<Merge_Images>().posChangeObj.GetComponent<Merge_Images>().placeNumb] = controlObject;
                        controlObject.GetComponent<Merge_Images>().placeNumb = controlObject.GetComponent<Merge_Images>().posChangeObj.GetComponent<Merge_Images>().placeNumb;
                        controlObject.GetComponent<Merge_Images>().posChangeObj.GetComponent<Merge_Images>().placeNumb = -1;
                    }
                }
            }
            else if(controlObject != null) {
                //controlObject.transform.position = downPosition;
                if(controlObject.GetComponent<Merge_Images>().placeNumb < 0) {
                    
                    controlObject.transform.position = downPosition;
                    
                }
                controlObject.GetComponent<Merge_Images>().isActive = false;
                controlObject.GetComponent<Merge_Images>().posChangeObj = null;
                controlObject.GetComponent<Merge_Images>().stayObject = null;
                controlObject.GetComponent<Merge_Images>().itemObject = null;
                controlObject = null;
            }
            isMouseItem = false;
            oldItemImages = null;
        }

        if(controlObject != null) {
            controlObject.transform.position = Input.mousePosition;
        }
        
    }

    private IEnumerator CreateMergeItems(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        if(createCount < 25) {
            if(merge_Collected[createCount] >= 0) {
                Instantiate(Resources.Load<GameObject>("Merge_Images/Merge_Img_" + (merge_Collected[createCount]).ToString()), createPos[createCount].position, Quaternion.identity, transform);
            }
            createCount++;
            createCroutine = CreateMergeItems(createTime);
            StartCoroutine(createCroutine);
        }
        else {
            
        }
    }
}
