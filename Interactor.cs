using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Mexico 19.419892, -99.088050
public class Narration {
    public string text;
    public float time;
    public Narration(float time, string text) {
        this.text = text;
        this.time = time;
    }
}
public class Interactor : MonoBehaviour {
    public string Stage = "SplashScreen";
    public GameObject InputJoystick, InputUseWeapon;
    public GameObject[] GridLayers;
    public Sprite PixelSprite, OverlaySprite;
    public GameObject SplashScreen;
    public GameObject LoadingScreen;
    public GameObject TutorialAssets;
    public AudioClip IntroMusic;
    AbstractMapController Map;
    List<Narration> Narration = new List<Narration> {
        new Narration(-60.00f, "We interrupt this\n"),
        new Narration(-59.50f, "We interrupt this\nprogram"),  
        new Narration(-59.00f, "to bring you a special\n"), 
        new Narration(-58.25f, "to bring you a special\nnews bulletin."),
        new Narration(-57.50f, "A state of emergency\n"),  
        new Narration(-56.50f, "A state of emergency\nhas been declared"), 
        new Narration(-55.75f, "by the President of\n"), 
        new Narration(-54.75f, "by the President of\nthe United States!"), 
        new Narration(-53.75f, "We're switching live to\n"), 
        new Narration(-53.00f, "We're switching live to\nWilsens Glenn,"),  
        new Narration(-52.25f, "New Jersey,\n"),
        new Narration(-51.50f, "New Jersey,\nwhere the landing of"), 
        new Narration(-50.50f, "hundreds of unidentified\n"),
        new Narration(-49.00f, "hundreds of unidentified\nspacecraft have now been"), 
        new Narration(-47.25f, "officially confirmed as\n"),
        new Narration(-46.25f, "officially confirmed as\na full scale invasion"), 
        new Narration(-45.75f, "of the Earth\n"), 
        new Narration(-45.00f, "of the Earth\nby Martians!"),
        new Narration(-43.00f, "⛈\n"),
        new Narration(-42.25f, "We're seeing ...\n"),
        new Narration(-41.25f, "We're seeing ...\nit's horrible"),
        new Narration(-40.25f, "I can't believe\n"),
        new Narration(-39.25f, "I can't believe\nmy eyes ..."),    
        new Narration(-38.25f, "People are dying ...\n"),   
        new Narration(-37.25f, "People are dying ...\nbeing trampled"),    
        new Narration(-36.25f, "in their efforts to\n"),    
        new Narration(-35.25f, "in their efforts to\nescape!"), 
        new Narration(-34.25f, "Power lines are down\n"),   
        new Narration(-33.25f, "Power lines are down\neverywhere!"),   
        new Narration(-32.25f, "We could be cut off at\n"),   
        new Narration(-31.25f, "We could be cut off at\nany minute!"),    
        new Narration(-30.25f, "⛈\n"),  
        new Narration(-29.25f, "There's another group of\n"),     
        new Narration(-28.25f, "There's another group of\nspaceships"),    
        new Narration(-27.25f, "There's another group of\nalien ships"),    
        new Narration(-26.25f, "They're coming out of\n"), 
        new Narration(-25.25f, "They're coming out of\nthe sky!"), 
        new Narration(-24.25f, "⛈\n"),
        new Narration(-21.00f, "Click / tap\n"),
        new Narration(-20.00f, "Click / tap\nto continue"),

        new Narration( 00.00f, "Today, you will learn\n"), // "Map Interface"
        new Narration( 02.00f, "Today, you will learn\nto use the Map Interface"),
        new Narration( 04.00f, "to issue tactical\n"),
        new Narration( 05.00f, "to issue tactical\ncommands."),
        new Narration( 07.00f, "Click / tap\n"),
        new Narration( 09.00f, "Click / tap\n\"Tutorial\""),

        new Narration( 60.00f, "Press the Zoom key\n"), // "Zoom In"
        new Narration( 61.50f, "Press the Zoom key\nto zoom in."),
        new Narration( 63.50f, "Click / tap\n"),
        new Narration( 64.50f, "Click / tap\n\"Zoom\""),

        new Narration(120.00f, "When the map is at\n"), // "Zooming In"
        new Narration(121.00f, "When the map is at\nmaximum zoom,"),
        new Narration(122.50f, "extra detail is\n"),
        new Narration(123.50f, "extra detail is\nrevealed,"),
        new Narration(124.75f, "such as fortifications\n"),
        new Narration(126.00f, "such as fortifications\nand individual planes."),
        
        new Narration(180.00f, "Welcome to the U.S.\n"), // "Welcome"
        new Narration(181.00f, "Welcome to the U.S.\nNaval Academy"),
        new Narration(182.50f, "Aerial Ordinance\n"), 
        new Narration(183.50f, "Aerial Ordinance\nTutorial"), 
        new Narration(185.00f, "Here, you will learn\n"),
        new Narration(186.25f, "Here, you will learn\nthe main ways"),
        new Narration(187.00f, "of attacking targets\n"),
        new Narration(188.50f, "of attacking targets\nfrom the air."),
        new Narration(189.50f, "So, be sure to pay\n"),
        new Narration(190.50f, "So, be sure to pay\nattention!"),

        new Narration(192.00f, "Aim the crosshair using\n"),
        new Narration(194.00f, "Aim the crosshair using\nthe mouse"),
        new Narration(195.00f, "or joystick.\n"),
        new Narration(196.00f, "When you're on target\n"),
        new Narration(197.00f, "When you're on target\nthe crosshair"),
        new Narration(198.00f, "alters shape to\n"),
        new Narration(199.00f, "alters shape to\nindicate a"),
        new Narration(200.00f, "lock-on.\n"),
        new Narration(202.00f, "Click / tap\n"),
        new Narration(203.00f, "Click / tap\n\"Printer\""),
        
        new Narration(240.00f, "You can also use the\n"),
        new Narration(240.75f, "You can also use the\ncrosshair"),
        new Narration(242.00f, "to issue orders to your\n"),
        new Narration(243.00f, "currently seleted unit.\n"),
        new Narration(245.00f, "Click / tap\n"),
        new Narration(246.00f, "Click / tap\n\"Print\""),

        new Narration(300.00f, "Your target will be\n"), // "Look around"
        new Narration(301.00f, "Your target will be\nthat cargo hulk there"),
        new Narration(302.00f, "While she moves into\n"),
        new Narration(303.00f, "While she moves into\nposition"),
        new Narration(304.00f, "Let's explain how\n"), 
        new Narration(305.00f, "Let's explain how\nartillery works."),
        new Narration(306.00f, "First off, try looking\n"),
        new Narration(307.00f, "First off, try looking\naround"),
        new Narration(308.00f, "360° awareness is needed\n"),
        new Narration(309.00f, "360° awareness is needed\nfor dogfighting."),
        new Narration(311.00f, "Click / tap\n"),
        new Narration(312.00f, "Click / tap\n\"Pan\" or Drag"),

        new Narration(360.00f, "You can also use the\n"), // "Target Button"
        new Narration(361.00f, "You can also use the\nTarget Button"),
        new Narration(362.00f, "to cycle targets\n"),
        new Narration(363.00f, "to cycle targets\nbetween all"),
        new Narration(364.00f, "detected enemy units.\n"),
        new Narration(366.00f, "Click / tap\n"),
        new Narration(367.00f, "Click / tap\n\"Target\""),

        // "Press the Use Weapon\n",
        // "Press the Use Weapon\ncontrol",
        // "to fire!\n",

        // "You can also shoot from\n",
        // "You can also shoot from\nBinocular View",
        // "which can help you aim\n",
        // "which can help you aim\nmore accurately.",

        // "Great!\n", 
        // "Time to get this baby\n",
        // "Time to get this baby\nairborn",
        // "Set your throttle to\n",
        // "Set your throttle to\nmaximum.",

        // "Excellence work!\n",
        // "You have now completed\n",
        // "You have now completed\nthe tutorial",
        // "I hope you never have\n",
        // "I hope you never have\ncause to use",
        // "the knowledge you have\n", 
        // "the knowledge you have\njust acquired.",
        // "That is all for today!\n",
        // "That is all for today!\nDismissed!",

        // "", // ""
        // "2",
        // "3",
        // "⛈", 0
        // "⛅", 2f
        // "7 ...", 6.75f
        // "6 ...", 7.75f
        // "5 ...", 8.75f
        // "4 ...", 9.75f
        // "3 ...", 10.75f
        // "2 ...", 11.75f
        // "1 ...", 12.75f
        // "☄ Tap to continue", 13.75f
        // "Today, orbitting", 18.5f
        // "satellites of the", 19.75f
        // "Navy Navigation", 20.75f
        // "Satellite System", 21.5f
        // "provide around-the-", 23.5f
        // "clock ultraprecise", 24.5f
        // "position fixes", 26.25f
        // "from space", 27.5f
        // "to units of", 28.5f
        // "the fleet,", 29.25f
        // "everywhere,", 30f
        // "in any kind", 31f
        // "of weather.", 32.25f
        // "⛈", 34.25f
        // "Navigation", 39f
        // "by satellite,", 40f
        // "how and why", 42f
        // "does it work?", 43.25f
        // "First, a little", 44.75f
        // "astrophysics", 45.5f
        // "to answer why.", 46.75f
        // "⛈", 48.25f
        // "Any satellite, man-", 54.5f
        // "made or not,", 56f
        // "remains in orbit", 57f
        // "because the force with", 58f
        // "which it is trying to", 59.5f
        // "fly away from Earth", 60.5f
        // "is matched by the", 62.25f
        // "gravitation pull", 63.25f
        // "of Earth.", 64.75f
        // "So it continues", 66.25f
        // "moving around Earth", 67.25f
        // "in an orbit whose", 68.75f
        // "path conforms very", 70.25f
        // "nearly to the", 71f
        // "classic laws of Sir", 72f
        // "Isaac Newton", 73f
        // "and Johannes Kepler", 74f
        // "Tap to continue ...", 76f
        // "⛈", 0
        // "⛅", 2f
        // "Suppose we put", 3.5f
        // "a radio transmitter", 4.5f
        // "in a satellite.", 5.5f
        // "You will detect", 9.25f
        // "that the radio", 10.25f
        // "frequency is", 11f
        // "doppler shifted", 11.75f
        // "while the satellite", 13f
        // "passes by.", 14f
        // "The doppler effect", 18.25f
        // "shows up as an", 19.25f
        // "apparent change in", 20.25f
        // "frequency, and", 21.25f
        // "is caused by the", 22.25f
        // "relative motion", 23.25f
        // "between the satellite", 24.5f
        // "transmitter and the", 25.25f
        // "receiving antenna", 27f
        // "on Earth.", 29f
        // "Tap to continue ...", 31
        // "⛅", 2f
        // "Doppler shift", 3.5f
        // "can be plotted", 4.5f
        // "frequency versus", 5.5f
        // "time.", 7.25f
        // "to produce a", 8.25f
        // "unique curve.", 9.25f
        // "Which can be", 10.75f
        // " received at only", 11.8f
        // "one point on Earth", 11.8f
        // "at a given instant.", 13.75f
        // "Knowing your position", 18f
        // "on Earth,", 18f
        // "you can use the", 20f
        // "Doppler Curve", 21.25f
        // "to calculate", 22.25f
        // "the exact orbit", 23f
        // "of the satellite", 24f
        // "And the reverse", 27f
        // "is true,", 28f
        // "if you start with", 30f
        // "the satellite of", 31f
        // "known orbit,", 31f
        // "analysis of the", 33f
        // "Doppler Shift as", 34f
        // "the satellite passes,", 35f
        // "will tell you exactly", 36.5f
        // "where you are,", 37.75f
        // "anywhere on Earth.", 39f
        // "This fact", 41f
        // "forms the basis", 42.5f
        // "of the Navy", 43.5f
        // "Navigation Satellite", 44.25f
        // "System.", 45.5f
        // "The system is more", 47.75f
        // "accurate than", 48.75f
        // "any other navigation", 50f
        // "system known today.", 51f
        // "Accuracy that can", 54.5f
        // "establish a pinpoint of", 55.5f
        // "latitude and longitude.", 57.5f
        // "This precision", 59.5f
        // "is the result", 60.5f
        // "of new techiques", 61.5f
        // "for fine control", 62.5f
        // "and measurement", 63.5f
        // "of time", 64.5f
        // "in terms of", 64.5f
        // "frequencies generated", 64.5f
        // "by ultrastable", 64.5f
        // "oscillators.", 64.5f
        // "Of course,", 71.75f
        // "There are many", 72.25f
        // "astronautical", 73.25f
        // "problems that", 74.25f
        // "confront this new", 75.5f
        // "system.", 76.5f
        // "Tap to continue ...", 78.5f
        // "⛈", 0
        // "⛅", 2f
        // "Series of experi-", 2.5f
        // "mental satellites.", 3.5f
        // "Beginning with the", 4.5f
        // "launch of the", 5.5f
        // "\"NAV-1B\" satellite", 6.25f
        // "in April, nineteen sixty.", 7.5f
        // "⛈", 9.5f
        // "To cover the whole Earth", 11f
        // "a satellite must be in", 12.5f
        // "near-polar orbit.", 13.5f
        // "That is, an", 15f
        // "orbit passing", 16f
        // "near the north and", 17f
        // "south poles.", 18f
        // "As the Earth", 20.5f
        // "rotates beneath the", 21.5f
        // "fixed plane of", 22.5f
        // "the orbit,", 23.5f
        // "which passes completely", 24.5f
        // "around Earth,", 25.75f
        // "Sooner or later", 28f
        // "the satellite will", 29.5f
        // "pass within", 30.5f
        // "range of any", 31.25f
        // "part of the globe.", 32.25f
        // "With one satellite", 35f
        // "in orbit,", 35.75f
        // "a particular point", 37.25f
        // "on the Earth", 38.25f
        // "is within range", 39.25f
        // "at least once", 40.25f
        // "each twelve hours.", 41.75f
        // "Ships and submarines", 46.5f
        // "need to know", 47.75f
        // "their position more", 48.25f
        // "frequently that this", 49.25f
        // "and therefore", 51.75f
        // "the \"Navy Navigation\"", 52.5f
        // "\"Satellite System\"", 53.75f
        // "employs a constell-", 54.75f
        // "ation of satellites,", 55.75f
        // "each in a polar orbit.", 57.5f
        // "Tap to continue ...", 59.5f
        // "⛈", 0
        // "⛅", 2f
        // "The message is", 3.5f
        // "injected into the", 4.25f
        // "satellite by", 5.25f
        // "high-power radio", 6.25f
        // "radio transmission.", 7f
        // "This updates", 11.5f
        // "old information", 12.5f
        // "stored in satellite", 14f
        // "memory,", 15f
        // "and extends the", 16.5f
        // "navigational utility", 17.25f
        // "of that satellite.", 18.5f
        // "The system works", 20f
        // "anywhere in the world", 21.25f
        // "night or day, in", 23f
        // "any kind of weather.", 25f
        // "For every pound", 35f
        // "of satellite in", 36f
        // "orbit, there are", 37f
        // "tons of equipment", 38f
        // "on Earth", 39f
        // "that make navigation", 40.25f
        // "by satellite", 41.25f
        // "possible.", 42.75f
        // "Tap to continue ...", 44.75f
        // "⛈", 0
        // "⛅", 2f
        // "We're inside the", 4.25f
        // "\"Operation Center\"", 5.25f
        // "command post for", 7.25f
        // "the \"Operations Duty\"", 8.25f
        // "\"Officer\".", 9.25f
        // "The mission of", 13f
        // "the group-", 13.5f
        // "around the clock,", 15f
        // "seven days a week,", 16f
        // "is to make sure", 17.5f
        // "that each navagation", 18.5f
        // "satellite", 19.5f
        // "always has", 20f
        // "correct, up-to-date", 21.5f
        // "information", 22.5f
        // "stored in its", 23.5f
        // "memory unit.", 24.5f
        // "An informative array", 28f
        // "of actuated displays", 29.25f
        // "shows the performance,", 31.25f
        // "memory,", 33.25f
        // "and injection status", 34f
        // "and helps the duty", 36f
        // "officer coordinate", 37f
        // "all network operations", 38.5f
        // "that keep the satellites", 40.5f
        // "broadcasting navigation", 41.5f
        // "data.", 43.25f
        // "Tap to continue ...", 45.24f
        // "⛈", 0
        // "⛅", 2f
        // "Satellite memory", 4f
        // "units and", 5f
        // "control circuitry", 6f
        // "can handle nearly", 7f
        // "twenty-five thousand", 8f
        // "separate bits", 9f
        // "of modulated", 10f
        // "information", 11f
        // "⛈", 12.5f
        // "The satellite", 16f
        // "gets its power", 17f
        // "from the sun.", 18f
        // "Sixteen thousand", 20f
        // "individual solar", 21f
        // "cells convert", 22f
        // "sunlight into", 23f
        // "electrical energy", 24f
        // "that is stored", 25f
        // "in Nickle-", 25.75f
        // "Cadium batteries", 26.75f
        // "inside the satellite.", 28f
        // "Tap to continue ...", 30
        // "⛈", 0
        // "⛅", 2f
        // "A few years later", 3.25f
        // "the French genius", 4.25f
        // "Blaise Pascal", 5.5f
        // "invented and built", 7.5f
        // "the world's first", 8.5f
        // "mechanical adding", 9.5f
        // "machines.", 10.5f
        // "The is one of them.", 11.5f
        // "Made in the sixteen-", 13f
        // "forties.", 14f
        // "Pascal's acheievement", 16.5f
        // "lay in the", 18f
        // "gear mechanism", 18.5f
        // "which automatically", 20f
        // "took care of carry-", 20.75f
        // "overs.", 21.75f
        // "For example,", 23.25f
        // "six", 25f
        // "plus nine", 28.5f
        // "and the one carried", 30.75f
        // "over to the next place.", 32f
        // "⛈", 34f
        // "In every area", 36.75f
        // "of defense,", 37f
        // "science,", 38.5f
        // "engineering and", 39.5f
        // "business,", 40.5f
        // "progress depends", 41.5f
        // "on the availability", 42.5f
        // "of fast, accurate", 43.75f
        // "methods of calculation.", 45f
        // "They've enabled us", 48.5f
        // "to take giant", 49.75f
        // "steps forward", 50.25f
        // "in power,", 51f
        // "in control,", 52.5f
        // "in design,", 54.5f
        // "in processing,", 55.5f
        // "and in research.", 57f
        // "Tap to continue ...", 59f
        // "These current tests", 35.25f
        // "are enabling", 36.75f
        // "N.A.S.A", 37.75f
        // "and the broadcasting", 38.25f
        // "community", 39.25f
        // "to iron out", 40.25f
        // "technical problems", 41.25f
        // "that are involved", 42.25f
        // "in this form of", 43.25f
        // "transmission.", 44.25f
        // "And to determine", 45.25f
        // "the costs of such", 46.75f
        // "future operations.", 47.75f
        // "If these tests are", 50f
        // "successful,", 51f
        // "we have every reason", 52.5f
        // "to believe that", 53.25f
        // "they will be,", 53.75f
        // "The American", 55.25f
        // "people will", 55.75f
        // "reap a major", 56.75f
        // "domestic dividend", 57.75f
        // "from the national", 59.25f
        // "space efforts.", 60.25f
        // "⛈", 61.25f
        // "The current goal", 87.25f
        // "of satellite", 88.25f
        // "geodesy", 89f
        // "is to tie all", 89.5f
        // "geodetic", 90.5f
        // "grids together", 91.25f
        // "within an", 92f
        // "accuracy of", 92.5f
        // "thirty feet.", 93.25f
        // "Using high-flying", 95f
        // "satellites as geo-", 96f
        // "detic markers,", 97f
        // "the world's", 98.5f
        // "contentients", 99f
        // "will eventually be", 99.5f
        // "tied together", 100.5f
        // "to one common", 101.25f
        // "reference system.", 102.25f
        // "Educational and", 104.5f
        // "cultural programs", 105.5f
        // "to populations of", 106.5f
        // "entire nations", 107.75f
        // "through inter-", 109.5f
        // "contentiential", 110f
        // "television!", 110.5f
        // "⛈", 112.5f
        // "As we develop", 114.4f
        // "this potential in", 115.5f
        // "the future,", 116.5f
        // "applications from", 117.5f
        // "space will have", 118.5f
        // "continued", 119.25f
        // "profound and", 120.25f
        // "direct effects", 121f
        // "on our", 122f
        // "everyday lives", 122.75f
        // "here on Earth.", 124f
        // "⛅", 126f
        // "⛈", 148
        // "⛈", 0
        // "⛅", 2.4f
        // "♩", 3.4f
        // "♩♩", 3.9f
        // "♪", 4.4f
        // "♪♪", 4.9f
        // "☄", 5.4f
        // "♪", 5.9f
        // "♫", 6.15f
        // "♫♪", 6.4f
        // "♫♫", 6.65f
        // "♫♫♪", 6.9f
        // "⛅", 8f
        // "In the final", 9f
        // "analysis however,", 10f
        // "the key to the future", 11f
        // "is not an aparatus", 12f
        // "a machine", 13.75f
        // "or an electronic cube,", 15f
        // "but the brainpower", 16f
        // "of man.", 17f
        // "Nothing will", 18.5f
        // "ever replace", 19f
        // "creative intelligence.", 20f
        // "In great laboratories,", 21.5f
        // "in colleges", 23.25f
        // "and universities,", 23.75f
        // "in solitary quiet ...", 25f
        // "Man thinks,", 26.35f
        // "reasons,", 28f
        // "experiments,", 29f
        // "creates.", 30f
        // "The mind", 31
        // "strains to peer", 32f
        // "beyond today's horizons", 33.25f
        // "for a glimpse of", 35f
        // "the wonders of tomorrow!", 36.25f
        // "⛅", 39f
        // "🔚", 43.5f
        // "⛈", 47f
    };
    float NarrationTimer = -60;
    int NarrationIndex = 0;
    // List<float> NarrationTiming = new List<float> {
    //     -60f,
    //     0f - 60f,
    //     0.75f - 60f,
    //     1.25f - 60f,
    //     2f - 60f,
    //     2.75f - 60f,
    //     3.5f - 60f,
    //     4.5f - 60f,
    //     5.25f - 60f,
    //     6.25f - 60f,
    //     7.25f - 60f,
    //     8f - 60f,
    //     8.75f - 60f,
    //     9.5f - 60f,
    //     10.5f - 60f,
    //     11.5f - 60f,
    //     12.5f - 60f,
    //     13.5f - 60f,
    //     14.5f - 60f,
    //     15.25f - 60f,
    //     16f - 60f,
    //     18.75f - 60f,
    //     19.25f - 60f,
    //     20.25f - 60f,
    //     20.75f - 60f,
    //     21.5f - 60f,
    //     22.25f - 60f,
    //     23.25f - 60f,
    //     24f - 60f,
    //     25.5f - 60f,
    //     26.5f - 60f,
    //     27.5f - 60f,
    //     28.5f - 60f,
    //     29f - 60f,
    //     30.25f - 60f,
    //     31.25f - 60f,
    //     32.25f - 60f,
    //     33f - 60f,
    //     34f - 60f,
    //     35 - 60f, 
    //     37 - 60f, 
    //     38 - 60f, 

    //     0, // Map Interface
    //     1.5f,
    //     3f,
    //     5f,
    //     7f, // Crosshairs
    //     9f,
    //     10f,
    //     11,
    //     12,
    //     13,
    //     14,
    //     15,

    //     60, // Zoom
    //     61.5f,

    //     120, // Zoom details
    //     121,
    //     122.5f,
    //     123.75f,
    //     124.75f,
    //     126.5f,
    //     127.5f,
    //     128.5f,

    //     // 180, // Welcome
    //     181f,
    //     182.5f,
    //     183.5f,
    //     185f,
    //     186.25f,
    //     187f,
    //     188.5f,
    //     189.5f,
    //     190.5f,
    //     191.5f,

    //     240,
    //     241,
    //     242,
    //     243,
    //     244,

        
    //     //
    //     // 123f,
    //     // 124.5f,
    //     // 150, // ""
    //     // 180, //"Welcome to the Tutorial"
    //     240
    // };
    public AudioClip NarratorSwitchToMap, NarratorZoomInMap, NarratorZoomInDetails, NarratorAimTheCrosshair, NarratorWelcome;
    public AudioClip TutorialOpening, TutorialLockOn, TutorialPrint, TutorialPrinted, TutorialComponentsIcons, TutorialLook, TutorialUseWeapon, TutorialBinocular, TutorialHitTarget, TutorialRightOn, TutorialAttackTarget, TutorialCycle, TutorialHulkDestroyed, TutorialTorpedoFired, TutorialCannonFired, TutorialSensorFired, TutorialTarget;
    public GameObject CampaignNewtonsLaws, CampaignDopplerShift, CampaignDopplerEffect, CampaignPlanksLaw, CampaignHawkingRadiation, CampaignMoracevsParadox, CampaignDeBroglieTheory, CampaignFermiParadox, CampaignPascalsWager;
    public AudioClip HookNarration, SplashScreenNarration, CampaignRadioDaysNarration, CampaignNewtonsLawsNarration, CampaignTheAtomNarration, CampaignDopplerShiftNarration, CampaignTheElectronNarration, CampaignDopplerEffectNarration, CampaignModernWarNarration, CampaignPlanksLawNarration, CampaignTelevisionNarration, CampaignHawkingRadiationNarration, CampaignVideotapeRecordsNarration, CampaignMoracevsParadoxNarration, CampaignElectronicMusicNarration, CampaignDeBroglieTheoryNarration, CampaignRadioIsotopesNarration, CampaignFermiParadoxNarration, CampaignHardnessTestNarration, CampaignPascalsWagerNarration, CampaignConclusionNarration, CampaignCreditsNarration;
    public GameObject Content, InterpreterPanel, InterpreterPanelEdge, MapPanel, SubtitlesShadow, Subtitles; 
    public AudioClip TutorialIntro, TutorialLookAround, TutorialMapInterface, TutorialMapScreen, TutorialIssueOrders, TutorialTargetWindow, TutorialTargetWindowHelp, TutorialTargetWindowSelected, TutorialGood, TutorialGood2, TutorialGood3, TutorialTry, TutorialBetter, TutorialCancel, TutorialOther, TutorialMusic, TutorialComponents, TutorialGetMoving, TutorialThrottle, TutorialDogfight, TutorialOutro, TutorialLeftWindow, TutorialRightWindow, TutorialCursor, TutorialSelect;
    public AudioClip CannonFire, ThrusterThrottle, SonarScan, TorpedoFact, ProcessorPing, GimbalRotate, TorpedoLaunch;
    public AudioClip ThemeSong, Click, Click2;
    public AudioClip SoundBack, SoundClick, SoundError, SoundOnMouse, SoundStart, SoundToggle, SoundProcessor, SoundGimbal, SoundCannon1, SoundCannon2, SoundCannon3, SoundRadar, SoundThruster, SoundBooster, SoundTorpedo1, SoundTorpedo2, SoundWarning, SoundWarningOver;
    public GameObject Overlay, OverlayZoomIn;
    public GameObject Example;
    public GameObject PrinterPrint, PrinterRight, PrinterLeft;
    private string command = "";
    private string history = "";
    public StructureController Ship;
    public GameObject volume_slider;
    public string start_text = "$"; 
    public OverlayInteractor OverlayInteractor;
    public GameObject ClickableText;
    Text TabToggle;
    GameObject MapScreenPanOverlay;
    public Text InputField;
    public Text Timer, TimerShadow, SplitTimer, SplitTimerShadow;
    GameObject camera;
    public List<GameObject> ButtonsCache = new List<GameObject>();
    int cache_size = 125;
    string audio_queue = "Splash Screen";
    public AudioClip clip_queue;

    // example ship ui
    // public GameObject InputUp, InputLeft, InputRight, InputDown, InputA, InputB;
    // example ship objects
    public GameObject CannonL, Processor, Bulkhead, BoosterR, ThrusterL, BoosterL, Thruster, ThrusterR, CannonR, SensorL, SensorR, Printer;
    // Start is called before the first frame update
    void Start()
    {
        InputJoystick.SetActive(false);
        InputUseWeapon.SetActive(false);

        PrinterPrint = GameObject.Find("InputPrinterPrint");
        PrinterRight = GameObject.Find("InputPrinterRight");
        PrinterLeft = GameObject.Find("InputPrinterLeft");
        OverlayZoomIn = GameObject.Find("OverlayZoomIn");
        TabToggle = GameObject.Find("TabToggle").GetComponent<Text>();
        SplashScreen.SetActive(true);
        Subtitles = GameObject.Find("Subtitles");
        SubtitlesShadow = GameObject.Find("SubtitlesShadow");
        SubtitlesShadow.SetActive(false);
        Subtitles.SetActive(false);
        camera = GameObject.Find("Main Camera");
        volume_slider = GameObject.Find("VolumeSlider");
        for (int i = 0; i < cache_size; i++) {
            ButtonsCache.Add(Instantiate(ClickableText, Content.transform) as GameObject);
        } 
        OverlayInteractor = GameObject.Find("OverlayBorder")?.GetComponent<OverlayInteractor>();
        InterpreterPanelEdge = GameObject.Find("InterpreterPanelEdge");
        MapScreenPanOverlay = GameObject.Find("MapScreenPanOverlay");
        RenderText("$ <b>tutorial</b>\n$");

        Timer.text = "⛅";
        PlayVideo(audio_queue);
        OnMapView();
        PrinterLeft.SetActive(false);
        PrinterRight.SetActive(false);
        PrinterPrint.SetActive(false);
        Printer.SetActive(false);

        Map = GameObject.Find("Map").GetComponent<AbstractMapController>();
    }
    
    public void HitSfx() {
        if (tutorial_clip_index == 6) {
            tutorial_clip_index = 7;
            tutorial_timer = 0;
            PlayAudio(TutorialHitTarget);
        }
    }
    public void PrinterLeftFx() {
        if (BoosterR.activeSelf)
        {
            Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){new ClassObj("◌")});
            BoosterL.SetActive(false);
            BoosterR.SetActive(false);
            SensorL.SetActive(true);
            SensorR.SetActive(true);
            ThrusterL.SetActive(true);
            ThrusterR.SetActive(true);
            Thruster.SetActive(false);
        } else if (CannonR.activeSelf) {
            Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){new ClassObj("◎")});
            CannonL.SetActive(false);
            CannonR.SetActive(false);
            BoosterL.SetActive(true);
            BoosterR.SetActive(true);
            Thruster.SetActive(true);
            ThrusterL.SetActive(false);
            ThrusterR.SetActive(false);
        } else {
            Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){new ClassObj("◍")});
            SensorL.SetActive(false);
            SensorR.SetActive(false);
            CannonL.SetActive(true);
            CannonR.SetActive(true);
        }

    }
    public void PrinterRightFx() {
        if (BoosterR.activeSelf)
        {
            Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){new ClassObj("◍")});
            BoosterL.SetActive(false);
            BoosterR.SetActive(false);
            CannonL.SetActive(true);
            CannonR.SetActive(true);
            ThrusterL.SetActive(true);
            ThrusterR.SetActive(true);
            Thruster.SetActive(false);
        } else if (CannonR.activeSelf) {
            Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){new ClassObj("◌")});
            CannonL.SetActive(false);
            CannonR.SetActive(false);
            SensorL.SetActive(true);
            SensorR.SetActive(true);
        } else {
            Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){new ClassObj("◎")});
            SensorL.SetActive(false);
            SensorR.SetActive(false);
            BoosterL.SetActive(true);
            BoosterR.SetActive(true);
            Thruster.SetActive(true);
            ThrusterL.SetActive(false);
            ThrusterR.SetActive(false);
        }
    }
    public void InputWFx() {
        if (Thruster.activeSelf) {
            GameObject.Find("Thruster").GetComponent<ComponentController>().Action(25);
        } else {
            GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(25);
            GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(25);
        }
    }
    public void InputDFx() {
        if (Thruster.activeSelf) {
            GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(25);
        } else {
            GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(25);
            GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-25);
        }

    }
    public void InputSFx() {
        if (Thruster.activeSelf) {
            GameObject.Find("Thruster").GetComponent<ComponentController>().Action(-25);
        } else {
            GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-25);
            GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(-25);
        }
        
    }
    public void InputAFx() {
        if (Thruster.activeSelf) {
            GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(25);
        } else {
            GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(25);
            GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(-25);
        }
    }
    public void InputXFx() {
        if (BoosterR.activeSelf)
        {
            GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(-1);
        } else if (CannonR.activeSelf) {
            GameObject.Find("CannonL").GetComponent<ComponentController>().Action(-1);
        } else {
            GameObject.Find("SensorL").GetComponent<ComponentController>().Action(-1);
        }
    }
    public void InputYFx() {
        if (tutorial_clip_index < 7) { 
            tutorial_timer = 0; tutorial_clip_index = 7; 
            if (BoosterR.activeSelf) PlayAudio(TutorialTorpedoFired); 
            if (CannonR.activeSelf) PlayAudio(TutorialCannonFired);
            if (SensorR.activeSelf) PlayAudio(TutorialCannonFired);
            
        }
        if (BoosterR.activeSelf)
        {
            GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(-1);
            GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-1);
        } else if (CannonR.activeSelf) {
            GameObject.Find("CannonL").GetComponent<ComponentController>().Action(-1);
            GameObject.Find("CannonR").GetComponent<ComponentController>().Action(-1);
        } else {
            GameObject.Find("SensorL").GetComponent<ComponentController>().Action(-1);
            GameObject.Find("SensorR").GetComponent<ComponentController>().Action(-1);
        }
        
    }
    bool printing = false;
    public void PrinterPrintFx() {
        if (InputField.text.Contains("Printer")) {
            printing = true;
            PrinterLeft.SetActive(false);
            PrinterRight.SetActive(false);
            PrinterPrint.SetActive(false);

            OverlayZoomIn.SetActive(true);
            //MapScreenPanOverlay.SetActive(true);
            
            if (NarrationTimer < 300) {
                NarrationTimer = 300;
                PlayAudio(TutorialTarget);
            }

        } else if (InputField.text.Contains("Processor")) {
            OnCodeView();
        } else if (InputField.text.Contains("Cannon")) {
            GameObject.Find("CannonL")?.GetComponent<ComponentController>().Action(-1);
            GameObject.Find("CannonR")?.GetComponent<ComponentController>().Action(-1);
            PrinterLeft.SetActive(false);
        } else if (InputField.text.Contains("Booster")) {
            GameObject.Find("BoosterL")?.GetComponent<ComponentController>().Action(-1);
            GameObject.Find("BoosterR")?.GetComponent<ComponentController>().Action(-1);
        } else if (InputField.text.Contains("Sensor")) {
            GameObject.Find("SensorL")?.GetComponent<ComponentController>().Action(-1);
            GameObject.Find("SensorR")?.GetComponent<ComponentController>().Action(-1);
        } else if (InputField.text.Contains("Thruster")) {
            GameObject.Find("ThrusterR")?.GetComponent<ComponentController>().Action(25);
            GameObject.Find("ThrusterL")?.GetComponent<ComponentController>().Action(25);
            GameObject.Find("Thruster")?.GetComponent<ComponentController>().Action(25);
        }
    }
    public static int Max(int val1, int val2) {
        return (val1>=val2)?val1:val2;
    }
    public static int Min(int val1, int val2) {
        return (val1<val2)?val1:val2;
    }
    float story_timer = -1f, start_timer = 0f;
    public void SetBackground(Color color) 
    {
        camera.GetComponent<Camera>().backgroundColor = color;
        for (int i = 0; i < GridLayers.Length; i++)
        {
            if (color.r == 0) {
                GridLayers[i].GetComponent<SpriteRenderer>().color = color;
                GridLayers[i].GetComponent<SpriteRenderer>().sprite = PixelSprite;
            }
            else {
                GridLayers[i].GetComponent<SpriteRenderer>().color = new Color(255/255f, 255/255f, 195/255f, .05f * (i + 2));//35f/255f, 95f/255f, 110f/255f, .66f);
                GridLayers[i].GetComponent<SpriteRenderer>().sprite = OverlaySprite;
            }
        }
    }
    public void SetVolume() 
    {
        camera.GetComponent<AudioSource>().volume = volume_slider.GetComponent<Slider>().value;
        GameObject.Find("Video Player").GetComponent<AudioSource>().volume = volume_slider.GetComponent<Slider>().value;
    }
    public void PlayMusic(AudioClip clip) {
        if (GameObject.Find("Video Player") != null) {
            GameObject.Find("Video Player").GetComponent<AudioSource>().clip = clip;
            GameObject.Find("Video Player").GetComponent<AudioSource>().Play();
            GameObject.Find("Video Player").GetComponent<AudioSource>().loop = false;
            GameObject.Find("Video Player").GetComponent<AudioSource>().volume = volume_slider.GetComponent<Slider>().value / 8;
        }
    }
    public void PlayAudio(AudioClip clip) {
        if (camera != null) {
            camera.GetComponent<AudioSource>().clip = clip;
            camera.GetComponent<AudioSource>().Play();
            camera.GetComponent<AudioSource>().loop = false;
        }
    }
    string queue_audio = "";
    public void PlayVideo(string url) 
    {
        LoadingScreen.SetActive(true);
        var trimmed_url = url.Replace(" ", "").Replace("'", "");
        queue_audio = trimmed_url;
        GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().enabled = true;
        // string asset_location = System.IO.Path.Combine (Application.streamingAssetsPath, "BitNaughts" + trimmed_url + "480p.mp4");
        // #if UNITY_WEBGL
        string asset_location = "https://raw.githubusercontent.com/bitnaughts/bitnaughts.assets/master/Videos/BitNaughts" + trimmed_url + "480p.mp4";
        // #endif
        // print (asset_location);
        GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().url = asset_location; 
        GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().Play();
        SetBackground(new Color(0f, 0f, 0f));
        MapScreenPanOverlay.SetActive(false);
        OverlayZoomIn.SetActive(false);
        volume_slider.SetActive(true);
        SubtitlesShadow.SetActive(true);
        Subtitles.SetActive(true);
        volume_slider.SetActive(true);
        // if (url.Length < 15) {
        //     InputField.text = "⧆ " + url;
        // }
        // else 
        // {
        //     InputField.text = url;
        // }
    }

    public void StoryMode(int index) {
        story_timer = 0f;
        start_timer = -1f;
        global_timer = 0f;
        clip_index = index;
        PlayVideo(campaign_clips[clip_index]);
        clip_index++;
        OnMapView();
    }
    public AudioClip LookupNarration(string clip) 
    {
        switch (clip)
        {
            case "Hook":
                return HookNarration;
            case "SplashScreen":
                return SplashScreenNarration;
            case "RadioDays":
                return CampaignRadioDaysNarration;
            case "NewtonsLaws":
                return CampaignNewtonsLawsNarration;
            case "TheAtom":
                return CampaignTheAtomNarration;
            case "DeBroglieTheory":
                return CampaignDeBroglieTheoryNarration;
            case "TheElectron":
                return CampaignTheElectronNarration;
            case "DopplerShift":
                return CampaignDopplerShiftNarration;
            case "ModernWar":
                return CampaignModernWarNarration;
            case "DopplerEffect":
                return CampaignDopplerEffectNarration;
            case "Television":
                return CampaignTelevisionNarration;
            case "PlanksLaw":
                return CampaignPlanksLawNarration;
            case "VideotapeRecords":
                return CampaignVideotapeRecordsNarration;
            case "HawkingRadiation":
                return CampaignHawkingRadiationNarration;
            case "ElectronicMusic":
                return CampaignElectronicMusicNarration;
            case "MoravecsParadox":
                return CampaignMoracevsParadoxNarration;
            case "RadioIsotopes":
                return CampaignRadioIsotopesNarration;
            case "FermiParadox":
                return CampaignFermiParadoxNarration;
            case "HardnessTest":
                return CampaignHardnessTestNarration;
            case "PascalsWager":
                return CampaignPascalsWagerNarration;
            case "Conclusion":
                return CampaignConclusionNarration;
            case "Credits":
                return CampaignCreditsNarration;
        }
        return SoundError;
    }
    public void OnToggleView() {
        if (MapPanel.activeSelf)
        {
            OnCodeView();
        }
        else 
        {
            OnMapView();
        }
    }
    public void OnMapView() {
        MapPanel.SetActive(true);
        if (volume_slider.activeSelf == false) MapScreenPanOverlay.SetActive(true);
        InterpreterPanel.SetActive(false);
        InterpreterPanelEdge.SetActive(false);
        TabToggle.text = "▦ GUI";
        volume_slider.SetActive(false);
    }
    public void OnCodeView() {
        MapPanel.SetActive(false);
        InterpreterPanel.SetActive(true);
        InterpreterPanelEdge.SetActive(true);
        TabToggle.text = "▤ TUI";
        volume_slider.SetActive(true);
    }
    public void AppendText(string text) {
        if (history.LastIndexOf("$") != -1) history = history.Substring(0, history.LastIndexOf("$"));
        history += text;
        RenderText(history);
    }
    public void ClearText() {
        if (history == "") history = "$";
        InputField.text = "☄ BitNaughts";
        MapScreenPanOverlay.SetActive(true);
        // volume_slider.SetActive(false/);
        RenderText(history);
    }
    public void ClearHistory() {
        history = "$";
        RenderText(history);
    }
    public void PrintMock() {
        Example.SetActive(true);
    }
    public void RenderText(string text) {

        foreach (var button in ButtonsCache) {
            button.SetActive(false);
        }
        float max_line_length = -1;
        string[] lines = text.Split('\n');
        for(int line = 0; line < lines.Length; line++) {
            string[] words = lines[line].Split(' ');
            int character_count = 0;
            for(int word = 0; word < words.Length; word++) {
                if (words[word].Length == 0) {
                    character_count++;
                    continue;
                } 
                character_count += words[word].Length + 1;
                if (words[word].StartsWith("<") && words[word].EndsWith(">")) character_count -= 7;
                InitializeClickableText(words[word], line, character_count);
            }
            if (character_count > max_line_length) {
                max_line_length = character_count;
            }
        }
        SetContentSize(max_line_length * 50f, lines.Length * 100f);
    }
    public string component_name = "";
    public string component_text = "";
    public void RenderComponent(string component) {
        var component_string = Ship.GetComponentToString(component);
        var component_header = component_string.IndexOf("\n");
        component_name = component_string.Substring(0, component_header);
        InputField.text = component_name;
        if (InputField.text == "Printer") {
            InputField.text = "▦ Printer";//" ⛴ Ship Select";
            if (GameObject.Find("0") != null) GameObject.Find("0").SetActive(false);
            PrinterLeft.SetActive(true);
            PrinterRight.SetActive(true);
            PrinterPrint.SetActive(true);
            PrinterPrint.transform.GetChild(0).GetComponent<Text>().text = "Print";
            if (NarrationTimer < 240) {
                NarrationTimer = 240;
                // tutorial_timer = 0; tutorial_clip_index = 2; 
                PlayAudio(TutorialComponentsIcons);
            }
            Processor.SetActive(true);
            Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){new ClassObj("◎")});
            Thruster.SetActive(true);
            BoosterL.SetActive(true);
            BoosterR.SetActive(true);
        }
        else if (InputField.text.Contains("Processor")) {
            InputField.text = "▩ " + InputField.text;
            PrinterPrint.SetActive(true);
            PrinterPrint.transform.GetChild(0).GetComponent<Text>().text = "▤ TUI";
            PrinterRight.SetActive(false);
            PrinterLeft.SetActive(false);
        }
        else if (InputField.text.Contains("Cannon")) {
            InputField.text = "◍ " + InputField.text;
            PrinterPrint.SetActive(true);
            PrinterPrint.transform.GetChild(0).GetComponent<Text>().text = "◍ Fire";
            PrinterRight.SetActive(false);
            PrinterLeft.SetActive(false);
        }
        else if (InputField.text.Contains("Booster")) {
            InputField.text = "◎ " + InputField.text;
            PrinterPrint.SetActive(true);
            PrinterPrint.transform.GetChild(0).GetComponent<Text>().text = "◎ Fire";
            PrinterRight.SetActive(true);
            PrinterLeft.SetActive(true);
        }
        else if (InputField.text.Contains("Sensor")) {
            InputField.text = "◌ " + InputField.text;
            PrinterPrint.SetActive(true);
            PrinterPrint.transform.GetChild(0).GetComponent<Text>().text = "◌ Fire";
            PrinterRight.SetActive(true);
            PrinterLeft.SetActive(true);
        }
        else if (InputField.text.Contains("Thruster")) {
            InputField.text = "◉ " + InputField.text;
            PrinterPrint.SetActive(true);
            PrinterPrint.transform.GetChild(0).GetComponent<Text>().text = "◉ Fire";
            PrinterRight.SetActive(false);
            PrinterLeft.SetActive(false);
        }
        if (GameObject.Find("OverlayDropdownLabel") != null) GameObject.Find("OverlayDropdownLabel").GetComponent<Text>().text = component_name;
        component_text = component_string.Substring(component_header + 1);
        // RenderText(component_text);
        // RenderText(Ship.interpreter.ToString());
    }

    public string[] GetComponents() {
        return Ship.GetControllers();
    }
    void SetContentSize(float width,float height) {
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(width + 100, height + 200);
    }
    public void SetInputPlaceholder(string placeholder) {
        InputField.text = "";//placeholder;
    }
    public string GetInput() {
        return InputField.text;
    }
    public void OnInput() {
        //create new component...
        // InputField.interactable = false;
        switch (GetCommand()) {
            case "nano":
                var component_gameObject = Instantiate(Overlay, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                component_gameObject.name = InputField.text;//object_reference.name + component_count;
                component_gameObject.GetComponent<SpriteRenderer>().size = new Vector2(2,2);//object_reference.GetComponent<ComponentController>().GetMinimumSize();
                component_gameObject.transform.SetParent(Ship.transform.Find("Rotator"));
                Ship.Start();
                OverlayInteractor.UpdateOptions();
                // OverlayInteractor.OnDropdownChange(); 
            break;
            default:
                GameObject.Find(component_name).name = InputField.text;
                Ship.Start();
                OverlayInteractor.UpdateOptions();
                // OverlayInteractor.OnDropdownChange(); 
            break;
        }
        // InputFieldPlaceholder.text = "";
    }
    public string GetCommand() {
        return command;
    }
    public void SetCommand(string command) {
        if (command == "$") {
            onLoad = false;
            ResetFlash("Clickable$", 0f);
        }
        this.command = command;
    }

    float timer = 0f;
    bool onLoad = true, aboutIntro = false, tutorialIntro = false, tutorialPan = false, tutorialTarget = false, tutorialFire = false, tutorialCancel = false, tutorialThrust = false, tutorialFinish = false, tutorialComplete = false;
    public bool TutorialRunning() {
        return tutorialIntro || tutorialPan || tutorialTarget || tutorialFire || tutorialCancel || tutorialThrust || tutorialFinish;
    }
    public void StartTutorial() {
        if (tutorialIntro == false) {
            onLoad = false;
            aboutIntro = false;
            tutorialIntro = true;
            tutorialPan = false;
            tutorialTarget = false;
            tutorialFire = false;
            tutorialCancel = false;
            tutorialThrust = false;
            tutorialFinish = false;
            timer = 0;
            global_timer = 0;
        }
    }
    public void UseWeapon() {
        Action("Cannon", -1);//GetInput(), -1);
        if (clip_index == 2 && campaign_stage == 2) { campaign_stage++; story_timer = 0f; }
    }
    public void CycleTutorial() {
        if (tutorial_clip_index == 10) {
            tutorial_clip_index = 11;
            tutorial_timer = 0;
            PlayAudio(TutorialGood2);
        } else if (tutorial_clip_index == 11) {
            tutorial_clip_index = 12;
            tutorial_timer = 0;
            PlayAudio(TutorialGood3);
        }
    }
    public void BinocularTutorial() {
        if (tutorial_clip_index == 9) {
            tutorial_clip_index = 10;
            tutorial_timer = 0;
            PlayAudio(TutorialCycle);
            // PlayAudio(TutorialGood2);
        }
    }
    public void PanTutorial() {
        if (NarrationTimer > 300 && NarrationTimer < 360) { 
            NarrationTimer = 360; 
            PlayAudio(TutorialCycle); 
            GameObject.Find("CycleToggle")?.SetActive(true);
            // InputJoystick.SetActive(true);

        }
        // if (clip_index == 2 && campaign_stage == 0) { campaign_stage++; story_timer = 0f; }
    }
    public void TargetTutorial() {
        if (clip_index == 2 && campaign_stage == 1) { 
            campaign_stage++; story_timer = 0f;
            
        }
        InputJoystick.SetActive(true);
    }
    public void FireTutorial() {
        if (clip_index == 2 && campaign_stage == 2) { campaign_stage++; story_timer = 0f; }
    }
    public void CancelTutorial() {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

    }
    public void ThrustTutorial() {
    }
    public void FinishTutorial() {
        Sound("Back");
        if (story_timer != -1) {
            campaign_splits[clip_index - 1] = story_timer;//999 ; in the future
            story_timer = 0f;
            if (clip_index < 20) {
                PlayVideo(campaign_clips[clip_index]);
                clip_index++;
            }
            else 
            {
                
                Application.Quit();
            }
        } else {
            Application.Quit();
        }
    }
    public void CompleteTutorial() {
        if (tutorialFinish) {
            tutorialComplete = true;
            tutorialIntro = false;
            tutorialPan = false;
            tutorialTarget = false;
            tutorialFire = false;
            tutorialCancel = false;
            tutorialThrust = false;
            tutorialFinish = false;
            onLoad = true;
            timer = 0;
            animation_timer = 0;
            MapSubtitlesAtTime("", 0f);
            SubtitlesAtTime("$ tutorial\n$", 0f);
            Sound("WarningOver");
            GetComponent<AudioSource>().Stop();
        }
    }
    public void Action(string name, int action) {
        GameObject.Find(name).GetComponent<ComponentController>().Action(action);
    }
    public void Sound(string clip) {
        switch (clip) {
            case "Back": Sound(SoundBack); break;
            case "Click": Sound(SoundClick); break;
            case "Error": Sound(SoundError); break;
            case "OnMouse": Sound(SoundOnMouse); break;
            case "Toggle": Sound(SoundToggle); break;
            case "Processor": Sound(SoundProcessor); break;
            case "Gimbal": Sound(SoundGimbal); break;
            case "Cannon1": Sound(SoundCannon1); break;
            case "Cannon2": Sound(SoundCannon2); break;
            case "Cannon3": Sound(SoundCannon3); break;
            case "Radar": Sound(SoundRadar); break;
            case "Booster": Sound(SoundBooster); break;
            case "Thruster": Sound(SoundThruster); break;
            case "Torpedo1": Sound(SoundTorpedo1); break;
            case "Torpedo2": Sound(SoundTorpedo2); break;
            case "Warning": Sound(SoundWarning); break;
            case "WarningOver": Sound(SoundWarningOver); break;
        }
    }

    public void PlayTheme() {
        timer = 0;
        global_timer = 0; 
        PlayThemeMusic();
        aboutIntro = true;
    }
    public void PlayThemeMusic() {
        GameObject.Find("World").GetComponent<AudioSource>().clip = ThemeSong;
        GameObject.Find("World").GetComponent<AudioSource>().volume = .05f;
        GameObject.Find("World").GetComponent<AudioSource>().Play();
    }
    public bool IsIntroCompleted() {
        return aboutIntro;
    }
    public void PlayGimbal() {
        Play(GimbalRotate);
    }
    public void PlayCannon() {
        Play(CannonFire);
    }
    public void PlayTorpedo() {
        Play(TorpedoLaunch);
    }
    public void PlayThruster() {
        Play(ThrusterThrottle);
    }
    public void PlayRadar() {
        Play(SonarScan);
    }
    public void PlayProcessor() {
        Play(ProcessorPing);
    }
    public void PlayClick() {
        Play(Click);
    }
    public void PlayClick2() {
        Play(Click2);
    }
    public void PlayAtTime(AudioClip clip, float time) {
        if (timer >= time && timer < time + (Time.deltaTime * 2f)) {
            Play(clip);
        }
    }
    public void Play(AudioClip clip) {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().volume = 1f;
        GetComponent<AudioSource>().Play();
    }
    public void Sound(AudioClip clip) {
        if (clip == null) {
            return;
        }
        if (camera == null) {
            camera = GameObject.Find("Main Camera");
        }
        // camera.GetComponent<AudioSource>().clip = clip;
        // camera.GetComponent<AudioSource>().volume = .5f;
        // camera.GetComponent<AudioSource>().Play();
    }
    void MapSubtitlesAtTime(string text, float time, float timer) {
        if (timer >= time && timer < time + (Time.deltaTime * 2f)) {
            Subtitles.GetComponent<Text>().text = text;
        }
    }
    void SubtitlesAtTime(string text, float time) {
        if (timer >= time && timer < time + (Time.deltaTime * 2f)) {
            // GameObject.Find("Subtitles").GetComponent<Text>().text = text + "\n";
            RenderText(text);
        }
    }
    void MapSubtitlesAtTime(string text, float time) {
        if (timer >= time && timer < time + (Time.deltaTime * 2f)) {
            Subtitles.GetComponent<Text>().text = text + "\n";
        }
    }
    void MapSubtitles(string text) {
        Subtitles.GetComponent<Text>().text = text;
    }
    double click_duration = 0;
    public double GetClickDuration() {
        return click_duration;
    }
    float global_timer = 0, animation_timer = 0;
    string FloatToTime(float time) {
        var pt = ((int)((time*100) % 60)).ToString("00");
        var ss = ((int)(time % 60)).ToString("00");
        var mm = (Mathf.Floor(time / 60) % 60).ToString();//.TrimStart('0');
        // if (mm == "") return ss + "." + pt;
        return mm + ":" + ss + "." + pt[0];
    }
    void Update () {
        animation_timer += Time.deltaTime;
        if (Input.GetMouseButton(0)) {
            click_duration += Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0)) {
            click_duration = 0;
        }
        if (Input.GetKeyDown("x")) {
            InputYFx();
        }
        if (Input.GetKey("w")) {
            if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(0.5f);
            if (tutorial_clip_index < 5) { tutorial_timer = 0; tutorial_clip_index = 5; PlayAudio(TutorialAttackTarget); InputUseWeapon.SetActive(true); InputJoystick.SetActive(true); }
        }
        if (Input.GetKey("q")) {
            if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(0.5f);
            if (tutorial_clip_index < 5) { tutorial_timer = 0; tutorial_clip_index = 5; PlayAudio(TutorialAttackTarget); InputUseWeapon.SetActive(true); InputJoystick.SetActive(true); }
        }
        if (Input.GetKey("e")) {
            if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-0.5f);
            if (tutorial_clip_index < 5) { tutorial_timer = 0; tutorial_clip_index = 5; PlayAudio(TutorialAttackTarget); InputUseWeapon.SetActive(true); InputJoystick.SetActive(true); }
        }
        if (Input.GetKey("a")) {
            if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(0.5f);
        }
        if (Input.GetKey("d")) {
            if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-0.5f);
        }
        if (Input.GetKey("s")) {
            if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-0.5f);
        }
        if (Input.GetKey("z")) {
            if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(0.5f);
        }
        if (Input.GetKey("c")) {
            if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-0.5f);
            if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(0.5f);
            if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-0.5f);
        }
        Gamepad gamepad = Gamepad.current;
        if (gamepad != null)
        {
            Vector2 stickL = gamepad.leftStick.ReadValue(); 
            if (stickL.y < -1/5f) {
                if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(stickL.y * 5);
                if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(stickL.y * 5);
                if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(stickL.y * 5);
                if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(stickL.y * 5);
                if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(stickL.y * 5);
            }
            if (stickL.y > 1/5f) {
                if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(stickL.y * 5);
                if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(stickL.y * 5);
                if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(stickL.y * 5);
                if (tutorial_clip_index < 5) { tutorial_timer = 0; tutorial_clip_index = 5; PlayAudio(TutorialAttackTarget); InputUseWeapon.SetActive(true); }
            }
            if (stickL.x < -1/5f) {
                if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(stickL.x * 5);
                if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-stickL.x * 5);
                if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(stickL.x * 5);
                if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-stickL.x * 5);
            }
            if (stickL.x > 1/5f) {
                if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(stickL.x * 5);
                if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-stickL.x * 5);
                if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(stickL.x * 5);
                if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-stickL.x * 5);
            }
        }
    }
    bool CheckInsideEdge() {
        return (Input.mousePosition.y > 100 && Input.mousePosition.y < Screen.height - 160 && Input.mousePosition.x > 100 && Input.mousePosition.x < Screen.width - 100);
    }
    string[] campaign_clips = new string[] { "Radio Days", "Newton's Laws", "The Atom", "De Broglie Theory", "The Electron",  "Doppler Effect", "Modern War", "Doppler Shift", "Television", "Plank's Law", "Videotape Records", "Hawking Radiation", "Electronic Music", "Moravec's Paradox", "Radio Isotopes", "Fermi Paradox", "Hardness Test", "Pascal's Wager", "Conclusion", "Credits", "" };
    string[] tutorial_clips = new string[] { "Tutorial Introduction", "Digital Computers", "Binary", "Components", "Morse Code", "☄ BitNaughts   " };
    int campaign_stage = -1, tutorial_stage = -1; 
    int[] campaign_clip_durations = new int[] {999, 81, 999, 79, 999, 64, 999, 46, 999, 79, 999, 74, 999, 107, 999, 95, 999, 116, 999, 51, 155, 999 };
    float[] campaign_splits = new float[20];
    float tutorial_timer = -1;
    float tutorial_save_time = 0;
    int[] tutorial_clip_durations = new int[] {999, 81, 999, 79, 999, 64, 999, 46, 999, 79, 999, 74, 999, 107, 999, 95, 999, 116, 999, 51, 999, 999, 999, 999 };
    int tutorial_clip_index = 0;
    int clip_index = 0;
    string credits_output = "";
    public void MapZoomed() {
        PlayMusic(IntroMusic);
        Stage = "MapZoomed";
        Printer.SetActive(true);
        Ship.Start();
        OverlayInteractor.UpdateOptions();
        // MapScreenPanOverlay.SetActive(true);
        GameObject.Find("OverlayPanDown")?.SetActive(false);
        GameObject.Find("BinocularToggle")?.SetActive(false);
        GameObject.Find("CycleToggle")?.SetActive(false);
        TutorialAssets.SetActive(true);
        GameObject.Find("OverlayZoomIn").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        PlayAudio(NarratorWelcome);
        NarrationTimer = 180;

        Map.Zoom(15);
    }
    public void MapZoom() {
        if (Stage == "MapZoom") {
            MapZoomed();
        } else {
            Stage = "MapZoom";
            PlayAudio(NarratorZoomInDetails);
            NarrationTimer = 120;
        }
        
    }
    public int MarkerIndex = 0;
    public void MapInteractor(string marker) {
        if (int.TryParse(marker, out MarkerIndex)) {
            if (MarkerIndex == 0) {
                GameObject.Find("0").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                PlayAudio(NarratorZoomInMap);
                NarrationTimer = 60;
                // NarrationIndex = 10;
            }
            OverlayZoomIn.SetActive(true);
            volume_slider.SetActive(false);
        }
    }
    void SpriteFlash(string name, float start) {
        if (tutorial_timer > start ) { 
            if (GameObject.Find(name) != null) GameObject.Find(name).GetComponent<SpriteRenderer>().color = new Color(.5f + (tutorial_timer * 2) % 1, .5f + (tutorial_timer * 2) % 1, 0, 1f);
        }
    }
    void Flash(string name, float start) {
        if (tutorial_timer > start) { 
            if (GameObject.Find(name) != null) GameObject.Find(name).GetComponent<Image>().color = new Color(.5f + (tutorial_timer * 2) % 1, .5f + (tutorial_timer * 2) % 1, 0, 1f);
        }
    }
    void ResetSpriteFlash(string name, float time) {
        if (tutorial_timer > time) { 
            if (GameObject.Find(name) != null) GameObject.Find(name).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
    void ResetFlash(string name, float time) {
        if (tutorial_timer > time) { 
            if (GameObject.Find(name) != null) GameObject.Find(name).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
    int print_index = 0;
    GameObject print_obj;
    void FixedUpdate()
    {
        global_timer += Time.deltaTime;
        if (InputField.text.Contains("Processor") && TabToggle.text == "▤ TUI") { 
            RenderText(Processor.GetComponent<ProcessorController>().interpreter.ToString());
        }
        if (queue_audio != "") {
            if (GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().frame != -1) {
                LoadingScreen.SetActive(false);
                PlayAudio(LookupNarration(queue_audio));
                queue_audio = "";
                if (story_timer != -1) {
                    story_timer = 0;
                }
                if (start_timer != -1) {
                    start_timer = 0;
                }
            }
        } else if (printing) {
            if (print_index < GameObject.Find("Example").transform.GetChild(0).GetComponentsInChildren<ComponentController>().Length) {
                if (print_obj == null) print_obj = GameObject.Find("Example").transform.GetChild(0).GetComponentsInChildren<ComponentController>()[print_index++].gameObject;
                if (GameObject.Find("Printer").GetComponent<PrinterController>().GoTo(print_obj.transform.localPosition + new Vector3(0, 0f, 0))) {
                    print_obj.GetComponent<ComponentController>().Launch();
                    print_obj = null;
                }
                else {
                    return;
                }
            } else {
                Printer.SetActive(false);
                ClearText();
                PrinterLeft.SetActive(false);
                PrinterRight.SetActive(false);
                PrinterPrint.SetActive(false);                
                Ship.Start();
                OverlayInteractor.UpdateOptions();

                // Ship.Start();
                // OverlayInteractor.UpdateOptions();
                // OverlayInteractor.OnDropdownChange("Printer"); 
                OverlayInteractor.gameObject.SetActive(false);
                // OverlayZoomIn.SetActive(true);
                MapScreenPanOverlay.SetActive(true);
                printing = false;
            }
        } else {// if (Stage == "MapInterface" || Stage == "MapZoom" || Stage == "MapZoomed") {
            // Wait for user interaction on Splash Screen
            if (NarrationTimer < -1 || NarrationTimer >= 0) NarrationTimer += Time.deltaTime;

            // if (Stage == "MapZoom") {
            // } else {
            Timer.text = FloatToTime(global_timer) + "\n" + System.DateTime.Now.AddYears(-54).ToString("MM/dd/yyyy") + "\n" + NarrationIndex + ":" + FloatToTime(NarrationTimer);
            // } 
            if (NarrationTimer < 0) // Splash Screen
            {
                if (Input.GetMouseButton(0) && CheckInsideEdge())
                {
                    NarrationTimer = 0;
                    Stage = "MapInterface";
                    InputField.text = "☄ BitNaughts";
                    SplashScreen.SetActive(false);
                    PlayAudio(NarratorSwitchToMap);
                    SetBackground(new Color(158/255f, 188/255f, 194/255f));
                    GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().enabled = false;
                    InputField.text = "☄ BitNaughts";
                }
            }
            bool updated = false;
            while (NarrationTimer >= Narration[NarrationIndex].time) {
                NarrationIndex++;
                updated = true;
            }
            if (updated) {
                MapSubtitles(Narration[NarrationIndex - 1].text);

                if (NarrationIndex == 68) {
                    PlayAudio(NarratorAimTheCrosshair);
                }           
                if (NarrationIndex == 90) {
                    PlayAudio(TutorialLookAround);
                }
                // switch (NarrationIndex) {
                //     case 43: 
                //     break;
                    // case 47:
                    //     PlayAudio(NarratorAimTheCrosshair);
                    // break;
                // }
            }
            if (NarrationTimer >= 0 && NarrationTimer < 60) {
                GameObject.Find("0").GetComponent<SpriteRenderer>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            }
            else if (NarrationTimer >= 60 && NarrationTimer < 180) {
                if (GameObject.Find("OverlayZoomIn") != null) GameObject.Find("OverlayZoomIn").GetComponent<Image>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            }
            else if (NarrationTimer >= 192 && NarrationTimer < 240) {
                GameObject.Find("0").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Printer";
                GameObject.Find("0").GetComponent<SpriteRenderer>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            }
            else if (NarrationTimer >= 240 && NarrationTimer < 300) {
                PrinterPrint.GetComponent<Image>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            }
                //     MapSubtitlesAtTime("╔═════════════════════╗\n║ BitNaughts Campaign ║\n║   * Report Card *   ║\n╠═════════════════════╣\n║ Time: " + FloatToTime(global_timer) + "\t\t  ║\n╚═════════════════════╝\n\nThanks for playing!", 0f
                //     MapSubtitlesAtTime("╔═════════════════════╗\n║ BitNaughts Campaign ║\n║   * Report Card *   ║\n╠═════════════════════╣\n║ Date: " + System.DateTime.Now.ToString("h:mm:ss.f") + "\t  ║\n╚═════════════════════╝\n\nThanks for playing!", 5f
                //     MapSubtitlesAtTime("╔═════════════════════╗\n║ BitNaughts Campaign ║\n║   * Report Card *   ║\n╠═════════════════════╣\n║ Date: " + System.DateTime.Now.ToString("MM/dd/yyyy") + "\t  ║\n╚═════════════════════╝\n\nThanks for playing!", 7.5f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Videos *     ║\n║ Woody Allen's      ║\n║ Radio Days         ║\n║             (1987) ║\n╚════════════════════╝\n\nTap to continue ...", 10f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Videos *     ║\n║ Jay Bonafield's    ║\n║ The Future Is Now  ║\n║             (1955) ║\n╚════════════════════╝\n\nTap to continue ...", 12.25f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Videos *     ║\n║ U.S. Navy's        ║\n║ Navigation Satel-  ║\n║ lite System (1955) ║\n╚════════════════════╝\n\nTap to continue ...", 14.5f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Videos *     ║\n║ U.S. Navy's        ║\n║ Digital Computer   ║\n║ Techniques  (1962) ║\n╚════════════════════╝\n\nTap to continue ...", 16.75f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Videos *     ║\n║ N.A.S.A's          ║\n║ Space Down to      ║\n║ Earth       (1970) ║\n╚════════════════════╝\n\nTap to continue ...", 19f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Sprite *     ║\n║ Alejandro Monge's  ║\n║ Modular Spaceships ║\n║             (2014) ║\n╚════════════════════╝\n\nTap to continue ...", 21.25f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Sound  *     ║\n║ Eidos Interactive  ║\n║ Battlestations     ║\n║ Pacific     (2009) ║\n╚════════════════════╝\n\nTap to continue ...", 23.5f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n║ Wintergatan's      ║\n║ Valentine          ║\n║             (2013) ║\n╚════════════════════╝\n\nTap to continue ...", 25.75f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n║ Wintergatan's      ║\n║ Valentine          ║\n║             (2013) ║\n╚════════════════════╝\n\nTap to continue ...", 28f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n║ Wintergatan's      ║\n║ Valentine          ║\n║             (2013) ║\n\n\nTap to continue ...", 33f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n║ Wintergatan's      ║\n║ Valentine          ║\n\n\n\nTap to continue ...", 33.5f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n║ Wintergatan's      ║\n\n\n\n\nTap to continue ...", 34f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n\n\n\n\n\nTap to continue ...", 34.5f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╚════════════════════╝\n\n\n\n\n\n\nTap to continue ...", 35f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     *   By   *     ║\n║ Brian Hungerman    ║\n║ brianhungerman.com ║\n║             (2022) ║\n╚════════════════════╝\n\nTap to continue ...", 62.25f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     *   By   *     ║\n║ Brian Hungerman    ║\n║ brianhungerman.com ║\n║             (2022) ║\n\n\nTap to continue ...", 85f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     *   By   *     ║\n║ Brian Hungerman    ║\n║ brianhungerman.com ║\n\n\n\nTap to continue ...", 85.5f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     *   By   *     ║\n║ Brian Hungerman    ║\n\n\n\n\nTap to continue ...", 86f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     *   By   *     ║\n\n\n\n\n\nTap to continue ...", 86.5f, story_timer);
                //     MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╚════════════════════╝\n\n\n\n\n\n\nTap to continue ...", 87f, story_timer);
        }
    }
    void InitializeClickableText(string text, int line, int pos) {
        foreach (var button in ButtonsCache) {
            if (button.activeSelf == false) {
                button.GetComponent<ClickableTextInteractor>().Initialize(this, OverlayInteractor, text, line, pos);
                break;
            }
        }
    }
}
