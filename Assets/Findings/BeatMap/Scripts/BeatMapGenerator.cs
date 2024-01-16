using UnityEngine;

[RequireComponent(typeof(AudioSource))]  // Ensure the GameObject has an AudioSource component
public class ClimbingLadder : MonoBehaviour
{
    const float rungSpacing = 3f; // constant
    const float ladderWidth = 5.0f; // constant
    const float numberOfSpheres = 4; // constant

    public AudioSource song;  // Audio clip for the song
    public float bpm = 120.0f;  // Beats per minute
    float beatsInSong;
    float numberOfRungs;
    float ladderHeight;

    Vector3 defaultPosition;

    public int startTime; // Where to start the song in seconds

    public static bool isPlaying = false;

    private void Awake()
    {
        // always run in the background
        Application.runInBackground = true;
        CalcParams();
        defaultPosition = transform.position;

    }

    // Start is called before the first frame update
    void Start()
    {
        song.time = startTime;
    }

    private void OnDrawGizmos()
    {
        CalcParams();
        DrawClimbingLadder(numberOfRungs);
    }

    void CalcParams()
    {
        //print("Song Length = " + song.clip.length);
        beatsInSong = (bpm / 60f) * song.clip.length;
        numberOfRungs = beatsInSong;
        ladderHeight = numberOfRungs * rungSpacing;

    }

    /// <summary>
    /// Visualize a beatmap with a climbing ladder
    /// </summary>
    /// <param name="numberOfRungs">BPM * 2</param>
    private void DrawClimbingLadder(float numberOfRungs)
    {
        Gizmos.color = Color.green;
        //print(numberOfRungs + " " + rungSpacing);
        
        //print("Ladder Height = " + ladderHeight);

        // bottom center position of the ladder
        Vector3 bottomCenter = transform.position;

        // Draw left side rail
        Gizmos.DrawLine(bottomCenter + new Vector3(-ladderWidth / 2f, 0, 0), bottomCenter + new Vector3(-ladderWidth / 2f, ladderHeight, 0));

        // Draw right side rail
        Gizmos.DrawLine(bottomCenter + new Vector3(ladderWidth / 2f, 0, 0), bottomCenter + new Vector3(ladderWidth / 2f, ladderHeight, 0));

        // Draw rungs
        for (int i = 0; i <= numberOfRungs; i++)
        {
            float y = i * rungSpacing;
            Vector3 rungStart = bottomCenter + new Vector3(-ladderWidth / 2, y, 0);
            Vector3 rungEnd = bottomCenter + new Vector3(ladderWidth / 2, y, 0);

            // every other rung is red
            if (i % 2 == 0)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.blue;
            }

            Gizmos.DrawLine(rungStart, rungEnd);

            // at each rung draw 4 spheres evenly spaced in between the rails
            float spacingBetweenSpheres = ladderWidth / (numberOfSpheres + 1);
            for (int j = 1; j <= numberOfSpheres; j++)
            {
                float sphereX = -ladderWidth / 2 + j * spacingBetweenSpheres;
                Gizmos.DrawSphere(bottomCenter + new Vector3(sphereX, y, 0), 0.1f);
            }
        }
    }


    private void Update()
    {
        if (song.isPlaying && isPlaying)
        {

            // keep track of the current time in the song
            float time = song.time;
            //move the ladder down such that the top of the ladder will reach the bottom of the screen at the end of the song
            transform.position = Vector3.Lerp(defaultPosition, defaultPosition + Vector3.down * ladderHeight, time / song.clip.length);
            print(time / song.clip.length);
        }
        
    }

}
