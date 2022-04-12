using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

using System.Linq;

public class Cursor : MonoBehaviour {

    GameObject settingobj;
    public bool debug,debug2,joycon,is_smallact;
    public GameObject myo = null;
    private Pose _lastPose = Pose.Unknown;
    public GameObject cube = null;
    private Quaternion ImaginaryCube;
    private Vector2 tergetAngle;
    public Vector2 correctionValues;
    public GameObject stick;
    [SerializeField]
    GameObject stickCube;
    [SerializeField]
    float[] areaLimitter;
    public float rad,kyori;

    //public GameObject controller_point;

    // A rotation that compensates for the Myo armband's orientation parallel to the ground, i.e. yaw.
    // Once set, the direction the Myo armband is facing becomes "forward" within the program.
    // Set by making the fingers spread pose or pressing "r".
    private Quaternion _antiYaw = Quaternion.identity;

    // A reference angle representing how the armband is rotated about the wearer's arm, i.e. roll.
    // Set by making the fingers spread pose or pressing "r".
    private float _referenceRoll = 0.0f;

    //JoYcon用
   // private static readonly Joycon.Button[] m_buttons =
     //  Enum.GetValues(typeof(Joycon.Button)) as Joycon.Button[];

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;
    private Joycon.Button? m_pressedButtonL;
    private Joycon.Button? m_pressedButtonR;

    // Use this for initialization
    void Start()
    {
        settingobj = GameObject.Find("Setting");
        if (joycon)
        {
            m_joycons = JoyconManager.Instance.j;

            if (m_joycons == null || m_joycons.Count <= 0) return;

            m_joyconL = m_joycons.Find(c => c.isLeft);
            m_joyconR = m_joycons.Find(c => !c.isLeft);
        }
    }

    // Update is called once per frame
    void Update()
    {

        float x = 0, y = 0;
        if (debug)
        {
            //非常用コントローラー
            x = Input.GetAxisRaw("Horizontal") * 2f;
            y = Input.GetAxisRaw("Vertical") * 2f;
            transform.position = new Vector3(transform.position.x + x * 0.2f, transform.position.y + y * 0.2f, 0.1f);
        }
        else if (debug2)
        {
            //非常用コントローラー
            x = Input.GetAxisRaw("Horizontal2") * 2f;
            y = Input.GetAxisRaw("Vertical2") * 2f;
            transform.position = new Vector3(transform.position.x + x * 0.2f, transform.position.y + y * 0.2f, 0.1f);
        }
        else if (joycon)
        {

        }
        else
        {
            //Myoを使った操作
            ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();


            bool updateReference = false;
            if (thalmicMyo.pose != _lastPose)
            {
                _lastPose = thalmicMyo.pose;
                //Debug.Log(_lastPose);
            }
            if (Input.GetButtonDown("Caribulation") || Input.GetButtonDown("StartLevel1")|| Input.GetButtonDown("StartLevel4")|| Input.GetButtonDown("StartLevel7")|| Input.GetButtonDown("StartLevel10"))
            {
                updateReference = true;
            }

            // Update references. This anchors the joint on-screen such that it faces forward away
            // from the viewer when the Myo armband is oriented the way it is when these references are taken.
            if (updateReference)
            {
                // _antiYaw represents a rotation of the Myo armband about the Y axis (up) which aligns the forward
                // vector of the rotation with Z = 1 when the wearer's arm is pointing in the reference direction.
                    _antiYaw = Quaternion.FromToRotation(
                        new Vector3(myo.transform.forward.x, 0, myo.transform.forward.z),
                        new Vector3(0, 0, 1)
                    );

                // _referenceRoll represents how many degrees the Myo armband is rotated clockwise
                // about its forward axis (when looking down the wearer's arm towards their hand) from the reference zero
                // roll direction. This direction is calculated and explained below. When this reference is
                // taken, the joint will be rotated about its forward axis such that it faces upwards when
                // the roll value matches the reference.
                Vector3 referenceZeroRoll = computeZeroRollVector(myo.transform.forward);
                _referenceRoll = rollFromZero(referenceZeroRoll, myo.transform.forward, myo.transform.up);
            }

            // Current zero roll vector and roll value.
            Vector3 zeroRoll = computeZeroRollVector(myo.transform.forward);
            float roll = rollFromZero(zeroRoll, myo.transform.forward, myo.transform.up);

            // The relative roll is simply how much the current roll has changed relative to the reference roll.
            // adjustAngle simply keeps the resultant value within -180 to 180 degrees.
            float relativeRoll = normalizeAngle(roll - _referenceRoll);

            // antiRoll represents a rotation about the myo Armband's forward axis adjusting for reference roll.
            Quaternion antiRoll = Quaternion.AngleAxis(relativeRoll, myo.transform.forward);

            // Here the anti-roll and yaw rotations are applied to the myo Armband's forward direction to yield
            // the orientation of the joint.
            ImaginaryCube = _antiYaw * antiRoll * Quaternion.LookRotation(myo.transform.forward);

            // The above calculations were done assuming the Myo armbands's +x direction, in its own coordinate system,
            // was facing toward the wearer's elbow. If the Myo armband is worn with its +x direction facing the other way,
            // the rotation needs to be updated to compensate.
            if (thalmicMyo.xDirection == Thalmic.Myo.XDirection.TowardWrist)
            {
                // Mirror the rotation around the XZ plane in Unity's coordinate system (XY plane in Myo's coordinate
                // system). This makes the rotation reflect the arm's orientation, rather than that of the Myo armband.
                ImaginaryCube = new Quaternion(transform.localRotation.x,
                                                    -transform.localRotation.y,
                                                    transform.localRotation.z,
                                                    -transform.localRotation.w);
            }

            cube.transform.position = new Vector2(stickCube.transform.position.x, stickCube.transform.position.y);
            stick.transform.rotation = ImaginaryCube;
            transform.position = new Vector2(cube.transform.position.x , cube.transform.position.y-1.37f );
            //transform.position = new Vector3(transform.position.x + cube.transform.position.x * 0.1f, transform.position.y + cube.transform.position.y * 0.1f,0.1f);
            //controller_point.transform.localPosition = new Vector2(cube.transform.position.x * 14, cube.transform.position.y * 14);

            /*if (round.gen.vive == 1)
            {
                thalmicMyo.Vibrate(VibrationType.Long);
                round.gen.vive = 0;
            }
            else if (round.gen.vive == 2)
            {
                thalmicMyo.Vibrate(VibrationType.Short);
                round.gen.vive = 0;
            }*/
        }


        //移動過程を音楽にする(仮)
        /*
        //横方向の座標に応じた効果音
        if (!MoveSound[0].isPlaying)
        {
            HorizontalSound();
            MoveSound[0].Play();
        }

        //縦方向の座標に応じた効果音
        if (!MoveSound[1].isPlaying)
        {
            VerticalSound();
            MoveSound[1].Play();
        }
        */

        //移動範囲制限
        if (transform.position.x > areaLimitter[0])
            transform.position = new Vector2(areaLimitter[0], transform.position.y);
        if (transform.position.x < areaLimitter[1])
            transform.position = new Vector2(areaLimitter[1], transform.position.y);
        if (transform.position.y > areaLimitter[2])
            transform.position = new Vector2(transform.position.x, areaLimitter[2]);
        if (transform.position.y < areaLimitter[3])
            transform.position = new Vector2(transform.position.x, areaLimitter[3]);
        //cube.transform.position = new Vector3(thalmicMyo.accelerometer.x*thalmicMyo.gyroscope.x*0.01f,thalmicMyo.accelerometer.y * thalmicMyo.gyroscope.y* 0.01f,0);

        float dx = transform.position.x;
        float dy = transform.position.y;
        rad = Mathf.Atan2(dy+1.37f, dx);
        kyori = Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.y + 1.37f, 2);
        //距離5未満で大きく動かすように注意喚起
        if (kyori < 6)
            is_smallact = true;
        else
            is_smallact = false;
    }

    // Compute a vector that points perpendicular to the forward direction,
    // minimizing angular distance from world up (positive Y axis).
    // This represents the direction of no rotation about its forward axis.
    Vector3 computeZeroRollVector(Vector3 forward)
    {
        Vector3 antigravity = Vector3.up;
        Vector3 m = Vector3.Cross(myo.transform.forward, antigravity);
        Vector3 roll = Vector3.Cross(m, myo.transform.forward);

        return roll.normalized;
    }

    // Compute the angle of rotation clockwise about the forward axis relative to the provided zero roll direction.
    // As the armband is rotated about the forward axis this value will change, regardless of which way the
    // forward vector of the Myo is pointing. The returned value will be between -180 and 180 degrees.
    float rollFromZero(Vector3 zeroRoll, Vector3 forward, Vector3 up)
    {
        // The cosine of the angle between the up vector and the zero roll vector. Since both are
        // orthogonal to the forward vector, this tells us how far the Myo has been turned around the
        // forward axis relative to the zero roll vector, but we need to determine separately whether the
        // Myo has been rolled clockwise or counterclockwise.
        float cosine = Vector3.Dot(up, zeroRoll);

        // To determine the sign of the roll, we take the cross product of the up vector and the zero
        // roll vector. This cross product will either be the same or opposite direction as the forward
        // vector depending on whether up is clockwise or counter-clockwise from zero roll.
        // Thus the sign of the dot product of forward and it yields the sign of our roll value.
        Vector3 cp = Vector3.Cross(up, zeroRoll);
        float directionCosine = Vector3.Dot(forward, cp);
        float sign = directionCosine < 0.0f ? 1.0f : -1.0f;

        // Return the angle of roll (in degrees) from the cosine and the sign.
        return sign * Mathf.Rad2Deg * Mathf.Acos(cosine);
    }

    // Adjust the provided angle to be within a -180 to 180.
    float normalizeAngle(float angle)
    {
        if (angle > 180.0f)
        {
            return angle - 360.0f;
        }
        if (angle < -180.0f)
        {
            return angle + 360.0f;
        }
        return angle;
    }

    // Extend the unlock if ThalmcHub's locking policy is standard, and notifies the given myo that a user action was
    // recognized.
    void ExtendUnlockAndNotifyUserAction(ThalmicMyo myo)
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (hub.lockingPolicy == LockingPolicy.Standard)
        {
            myo.Unlock(UnlockType.Timed);
        }

        myo.NotifyUserAction();
    }
}
