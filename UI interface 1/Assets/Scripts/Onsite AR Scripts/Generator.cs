using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    //Initial Values
    private float _size;
    private float _rotation;
    private float _transparency;
    private float _metalicness;

    [Header("Prefabs")] public GameObject boundingBox;
    public GameObject pavilionPrefab;
    [Header("Sliders")] public Slider size;
    public Slider rotation;
    public Slider transparency;
    public Slider metalicness;

    private List<GameObject> pavilions;
    private List<float> rotationIncrements;
    private List<float> transparencyIncrements;
    private List<float> metalicnessIncrements;
    
    void Start()
    {
        _size = size.value;
        _rotation = rotation.value;
        _transparency = transparency.value;
        _metalicness = metalicness.value;
        
        pavilions = new List<GameObject>();
        rotationIncrements = new List<float>();
        transparencyIncrements = new List<float>();
        metalicnessIncrements = new List<float>();
    }

    void Update()
    {
        if (size.value != _size)
        {
             _size = size.value;
             GenerateAggregation();
        }
     
        if (rotation.value != _rotation)
        {
            _rotation = rotation.value;
            RotateObjects();
        }
        
        if (transparency.value != _transparency)
        {
             _transparency = transparency.value;
             TweakTransparency();
        }
        
        if (metalicness.value != _metalicness)
        {
             _metalicness = metalicness.value;
             TweakMetalicness();
        }
    }
    
    void GenerateAggregation()
    {
        if (transform.childCount > 0)
        {
            foreach(Transform child in transform)
                Destroy(child.gameObject);
            pavilions.Clear();
            rotationIncrements.Clear();
            transparencyIncrements.Clear();
            metalicnessIncrements.Clear();
        }

        int height = 1;

        if (_size < 3)
            height = 1;
        else if (_size < 6)
            height = 2;
        else
            height = 3;

        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                for (int k = 0; k < height; k++)
                {
                    if (i == 0 && j == 0 && k == 0)
                        continue;
                    
                    float rotIncr = Random.Range(0, rotation.maxValue);
                    float transIncr = Random.Range(0, transparency.maxValue);
                    float metIncr = Random.Range(0, metalicness.maxValue);
                    
                    rotationIncrements.Add(rotIncr);
                    transparencyIncrements.Add(transIncr);
                    metalicnessIncrements.Add(metIncr);
                    
                    Vector3 pos = new Vector3(transform.position.x - i * boundingBox.transform.localScale.x,
                        transform.position.y + k * boundingBox.transform.localScale.y,
                        transform.position.z + j * boundingBox.transform.localScale.z);
                    var gb = Instantiate(pavilionPrefab, pos, Quaternion.Euler(0,180,0), transform);
                    gb.SetActive(true);
                    pavilions.Add(gb);
                }
            }
        }
        
    }
    
    private void RotateObjects()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < pavilions.Count; i++)
            {
                float degrees = Remap(rotation.value, 0, rotation.maxValue, 0, rotationIncrements[i]);
                pavilions[i].transform.rotation = Quaternion.Euler(0, degrees, 0);
            }
        }
    }
    
    private void TweakTransparency()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < pavilions.Count; i++)
            {
                float transp = Remap(transparency.value, 0, transparency.maxValue, 0, transparencyIncrements[i]);
                Transform currentPav = pavilions[i].transform;

                Color col = pavilions[i].gameObject.GetComponent<MeshRenderer>().material.color;
                col = new Color(col.r, col.g, col.b, transp);
                pavilions[i].gameObject.GetComponent<MeshRenderer>().material.color = col;
            }
        }
    }
    
    private void TweakMetalicness()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < pavilions.Count; i++)
            {
                float met = Remap(metalicness.value, 0, metalicness.maxValue, 0, metalicnessIncrements[i]);
                pavilions[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", met);
                pavilions[i].gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Glossiness", met);
            }
        }
    }
     
    public float Remap (float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}