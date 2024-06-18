using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperUtilities
{
    #region Math
    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);

        float degrees = radians * Mathf.Rad2Deg;

        return degrees;
    }

    public static Vector3 GetDirectionVectorFromAngle(float angle)
    {
        Vector3 directionVector = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0f);

        return directionVector;
    }
    #endregion Math

    #region ValidationChecks
    public static bool ValidateCheckEmptyString(Object thisObject, string fieldName, string stringToCheck)
    {
        if(stringToCheck == "")
        {
            Debug.Log(thisObject.name.ToString() + ": " + fieldName + " has empty string");
            return true;
        }
        return false;
    }

    public static bool ValidateCheckNullReference(Object thisObject, string fieldName, Object objectToCheck)
    {
        if(objectToCheck == null)
        {
            Debug.Log(thisObject.name.ToString() + ": " + fieldName + " has null values");
            return true;
        }
        return false;
    }

    public static bool ValidateCheckEnumerableValues(Object thisObject, string fieldName, IEnumerable enumerableObjectToCheck)
    {
        bool error = false;
        int count = 0;

        foreach(var item in enumerableObjectToCheck)
        {
            if(item == null)
            {
                Debug.Log(thisObject.name.ToString() + ": " + fieldName + " has null values in enumerable");
                error = true;
            }
            else
            {
                count++;
            }
        }

        if(count == 0)
        {
            Debug.Log(thisObject.name.ToString() + ": " + fieldName + " has empty values in enumerable");
            error = true;
        }

        return error;
    }

    public static bool ValidateCheckPositiveValue(Object thisObject, string fieldName, float valueToCheck, bool isZeroAllowed)
    {
        bool error = false;

        if (isZeroAllowed)
        {
            if (valueToCheck < 0)
            {
                Debug.Log(thisObject.name.ToString() + ": " + fieldName + " has value lower then 0");
                error = true;
            }
        }
        else
        {
            if (valueToCheck <= 0)
            {
                Debug.Log(thisObject.name.ToString() + ": " + fieldName + " has value lower or equal to 0");
                error = true;
            }
        }

        return error;
    }

    public static bool ValidateCheckZeroVector2(Object thisObject, string fieldName, Vector2 valueToCheck)
    {
        if(valueToCheck == Vector2.zero)
        {
            Debug.Log(thisObject.name.ToString() + ": " + fieldName + " has value of vector2 zero");
            return true;
        }
        return false;
    }

    #endregion
}
