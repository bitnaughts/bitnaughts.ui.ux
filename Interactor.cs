using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Starting Center 44.3812661305678, -97.9222112121185


// Mexico 19.419892, -99.088050


// 47.43855, -122.3071241 SEATAC
// 38.870983, -77.055977 Penatgon
// 14.52437664,-138.5327714
// 21.366984, -157.956068

// 28.2095913577275, -177.37936452302
// 35.05219871545,-196.802661046
// 41.8948060731725,-216.22595756898
// -33.939937, 151.175268
// 52.35932, 0.47298
// 37.454082, 126.435638

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
    public GameObject TutorialAssets, CampaignIntroAssets, CampaignIntroSatelliteAssets,  CampaignGroverAssets, CampaignGroverSatelliteAssets, CampaignPearlAssets, CampaignMidwayAssets, CampaignNexusAssets, CampaignAbyssAssets;
    public AudioClip SplashScreenComplete, SplashScreenHint, WarOfTheWorldsTryAgain, WarOfTheWorldsTheme, WarOfTheWorldsGetMoving, WarOfTheWorldsHeatRay, WarOfTheWorldsGood, WarOfTheWorldsStinger, WarOfTheWorldsMapScreen, WarOfTheWorldsTargetWindow, WarOfTheWorldsTargetWindowGood, WarOfTheWorldsTargetWindowIssueOrder, WarOfTheWorldsBeep, WarOfTheWorldsBeepBoop, WarOfTheWorldsClick, WarOfTheWorldsHum, WarOfTheWorldsCredit, WarOfTheWorldsIntro, WarOfTheWorldsFirstContact, WarOfTheWorldsRedCross, WarOfTheWorldsGroversMill, LoadingNarration, IntroMusic, TutorialIntroduction, CampaignNexus, CampaignAbyss, CampaignCosmos0, CampaignCosmos1, CampaignCosmos2, CampaignCosmos3, CampaignCosmos4, CampaignCosmos5, CampaignPearlIntroduction, CampaignMidwayIntroduction;
    public int CampaignIndex = 0;
    AbstractMapController Map;
    List<Narration> Narration = new List<Narration> {
        // "Campaign Intro"
        new Narration(-20.00f, "<b>⛅</b>"),
        // new Narration(-59.00f, "<b>We_know_now_that_in</b>"),
        // new Narration(-58.25f, "We_know_now_that_in\n<b>the_early_years_of_the</b>"),
        // new Narration(-57.50f, "We_know_now_that_in\nthe_early_years_of_the\n<b>twentieth_century_...</b>"), 
        // new Narration(-56.50f, "We_know_now_that_in\nthe_early_years_of_the\ntwentieth_century_..."), 
        // new Narration(-55.00f, "We_know_now_that_in\nthe_early_years_of_the\ntwentieth_century_...\n\n<b>This_world_was_being</b>"),
        // new Narration(-54.00f, "We_know_now_that_in\nthe_early_years_of_the\ntwentieth_century_...\n\nThis_world_was_being\n<b>watched_closely_by</b>"),  
        // new Narration(-52.75f, "We_know_now_that_in\nthe_early_years_of_the\ntwentieth_century_...\n\nThis_world_was_being\nwatched_closely_by\n<b>intelligences_...</b>"), 
        // new Narration(-52.00f, "We_know_now_that_in\nthe_early_years_of_the\ntwentieth_century_...\n\nThis_world_was_being\nwatched_closely_by\nintelligences_..."), 
        // new Narration(-51.00f, "We_know_now_that_in\nthe_early_years_of_the\ntwentieth_century_...\n\nThis_world_was_being\nwatched_closely_by\nintelligences_...\n\n<b>Greater_than_man!</b>"), 
        // new Narration(-49.50f, "<b>We_know_now_that</b>"), 
        // new Narration(-48.50f, "We_know_now_that\n<b>as_human-beings_busied</b>"), 
        // new Narration(-47.00f, "We_know_now_that\nas_human-beings_busied\n<b>themselves_about_their</b>"), 
        // new Narration(-46.00f, "We_know_now_that\nas_human-beings_busied\nthemselves_about_their\n<b>various_concerns_...</b>"),
        // new Narration(-45.00f, "We_know_now_that\nas_human-beings_busied\nthemselves_about_their\nvarious_concerns_..."),
        // new Narration(-44.00f, "We_know_now_that\nas_human-beings_busied\nthemselves_about_their\nvarious_concerns_...\n\n<b>They_were_scruntizied</b>"),
        // new Narration(-43.00f, "We_know_now_that\nas_human-beings_busied\nthemselves_about_their\nvarious_concerns_...\n\nThey_were_scruntizied\n<b>and_studied_...</b>"),
        // new Narration(-43.00f, "We_know_now_that\nas_human-beings_busied\nthemselves_about_their\nvarious_concerns_...\n\nThey_were_scruntizied\n<b>and_studied_...</b>"),
        // new Narration(-42.00f, "We_know_now_that\nas_human-beings_busied\nthemselves_about_their\nvarious_concerns_...\n\nThey_were_scruntizied\nand_studied_..."),
        // new Narration(-41.25f, "<b>Perhaps_almost_as</b>"),
        // new Narration(-40.50f, "Perhaps_almost_as\n<b>narrowly_as_a_man</b>"),
        // new Narration(-39.50f, "Perhaps_almost_as\nnarrowly_as_a_man\n<b>with_a_microscope</b>"),
        // new Narration(-38.25f, "Perhaps_almost_as\nnarrowly_as_a_man\nwith_a_microscope\n<b>might_scruntinize_the</b>"),
        // new Narration(-37.25f, "Perhaps_almost_as\nnarrowly_as_a_man\nwith_a_microscope\nmight_scruntinize_the\n<b>transient_creatures</b>"),
        // new Narration(-36.25f, "Perhaps_almost_as\nnarrowly_as_a_man\nwith_a_microscope\nmight_scruntinize_the\ntransient_creatures\n<b>that_swarm_and_multiply</b>"),
        // new Narration(-34.75f, "Perhaps_almost_as\nnarrowly_as_a_man\nwith_a_microscope\nmight_scruntinize_the\ntransient_creatures\nthat_swarm_and_multiply\n<b>in_a_drop_of_water_...</b>"),
        // new Narration(-33.75f, "Perhaps_almost_as\nnarrowly_as_a_man\nwith_a_microscope\nmight_scruntinize_the\ntransient_creatures\nthat_swarm_and_multiply\nin_a_drop_of_water_..."),
        // new Narration(-32.00f, "<b>With_infinite_complacence</b>"),
        // new Narration(-30.00f, "With_infinite_complacence\n<b>people_went_to_and_fro</b>"),
        // new Narration(-29.25f, "With_infinite_complacence\npeople_went_to_and_fro\n<b>over_the_Earth_about</b>"),
        // new Narration(-28.00f, "With_infinite_complacence\npeople_went_to_and_fro\nover_the_Earth_about\n<b>their_little_affairs_...</b>"),
        // new Narration(-27.50f, "With_infinite_complacence\npeople_went_to_and_fro\nover_the_Earth_about\ntheir_little_affairs_..."),
        // new Narration(-26.50f, "With_infinite_complacence\npeople_went_to_and_fro\nover_the_Earth_about\ntheir_little_affairs_...\n\n<b>Which_by_chance_or</b>"),
        // new Narration(-26.00f, "With_infinite_complacence\npeople_went_to_and_fro\nover_the_Earth_about\ntheir_little_affairs_...\n\nWhich_by_chance_or\n<b>design,_man_has</b>"),
        // new Narration(-24.00f, "With_infinite_complacence\npeople_went_to_and_fro\nover_the_Earth_about\ntheir_little_affairs_...\n\nWhich_by_chance_or\ndesign,_man_has\n<b>inherited_out_of_the</b>"),
        // new Narration(-23.00f, "With_infinite_complacence\npeople_went_to_and_fro\nover_the_Earth_about\ntheir_little_affairs_...\n\nWhich_by_chance_or\ndesign,_man_has\ninherited_out_of_the\n<b>dark_mystery_of_time</b>"),
        // new Narration(-21.50f, "With_infinite_complacence\npeople_went_to_and_fro\nover_the_Earth_about\ntheir_little_affairs_...\n\nWhich_by_chance_or\ndesign,_man_has\ninherited_out_of_the\ndark_mystery_of_time\n<b>and_space_...</b>"),
        new Narration(-19.00f, "<b>Across_an_immense</b>"),
        new Narration(-18.00f, "Across_an_immense\n<b>ethereal_gulf_...</b>"),
        new Narration(-17.00f, "Across_an_immense\nethereal_gulf_..."),
        new Narration(-16.00f, "Across_an_immense\nethereal_gulf_...\n\n<b>Intellect,</b>"),
        new Narration(-15.00f, "Across_an_immense\nethereal_gulf_...\n\nIntellect, <b>vast,_cool</b>"),
        new Narration(-13.00f, "Across_an_immense\nethereal_gulf_...\n\nIntellect,_vast,_cool\n<b>and_unsympathetic,</b>"),
        new Narration(-11.50f, "Across_an_immense\nethereal_gulf_...\n\nIntellect,_vast,_cool\nand_unsympathetic,\n<b>regarded_this_Earth</b>"),
        new Narration(-10.00f, "Across_an_immense\nethereal_gulf_...\n\nIntellect,_vast,_cool\nand_unsympathetic,\nregarded_this_Earth\n<b>with_envious_eyes,_and</b>"),
        new Narration(-07.50f, "Across_an_immense\nethereal_gulf_...\n\nIntellect,_vast,_cool\nand_unsympathetic,\nregarded_this_Earth\nwith_envious_eyes,_and\n<b>slowly_and_surely_drew</b>"),
        new Narration(-06.50f, "Across_an_immense\nethereal_gulf_...\n\nIntellect,_vast,_cool\nand_unsympathetic,\nregarded_this_Earth\nwith_envious_eyes,_and\nslowly_and_surely_drew\n<b>their_plans_against_us!</b>"),
        new Narration(-05.50f, "Across_an_immense\nethereal_gulf_...\n\nIntellect,_vast,_cool\nand_unsympathetic,\nregarded_this_Earth\nwith_envious_eyes,_and\nslowly_and_surely_drew\ntheir_plans_against_us!"),
        new Narration(-03.00f, "<b>⛈</b>"),
        new Narration(-02.00f, "⛈"),
        // new Narration(-47.00f, "We're_switching_live_to\nWilson_Glen,_New_Jersey\nwhere_the_landing_of\nhundreds_of_unidentified\nspacecraft_have_now_been\n<b>officially_confirmed_as</b>\n"),
        // new Narration(-46.00f, "We're_switching_live_to\nWilson_Glen,_New_Jersey\nwhere_the_landing_of\nhundreds_of_unidentified\nspacecraft_have_now_been\nofficially_confirmed_as\n<b>a_full_scale_invasion</b>\n"), 
        // new Narration(-45.50f, "We're_switching_live_to\nWilson_Glen,_New_Jersey\nwhere_the_landing_of\nhundreds_of_unidentified\nspacecraft_have_now_been\nofficially_confirmed_as\na_full_scale_invasion\n<b>of_Earth_by_Martians!</b>"), 
        // new Narration(-42.25f, "<b>We're_seeing_..._it's</b>\n"),
        // new Narration(-41.25f, "We're_seeing_..._it's\n<b>horrible_..._I_can't</b>\n"),
        // new Narration(-40.25f, "We're_seeing_..._it's\nhorrible_..._I_can't\n<b>believe_my_eyes_...</b>"),
        // new Narration(-39.25f, "We're_seeing_..._it's\nhorrible_..._I_can't\nbelieve_my_eyes_...\n"),    
        // new Narration(-38.25f, "We're_seeing_..._it's\nhorrible_..._I_can't\nbelieve_my_eyes_...\n<b>People_are_dying_...</b>\n"),   
        // new Narration(-37.25f, "We're_seeing_..._it's\nhorrible_..._I_can't\nbelieve_my_eyes_...\nPeople_are_dying_...\n<b>being_trampled_in_their</b>\n"),    
        // new Narration(-36.25f, "We're_seeing_..._it's\nhorrible_..._I_can't\nbelieve_my_eyes_...\nPeople_are_dying_...\nbeing_trampled_in_their\n<b>efforts_to_escape!</b>"),    
        // new Narration(-35.25f, "We're_seeing_..._it's\nhorrible_..._I_can't\nbelieve_my_eyes_...\nPeople_are_dying_...\nbeing_trampled_in_their\nefforts_to_escape!"), 
        // new Narration(-34.25f, "<b>Power_lines_are_down</b>\n"),   
        // new Narration(-33.25f, "Power_lines_are_down\n<b>everywhere!_We_could_be</b>\n"),   
        // new Narration(-32.25f, "Power_lines_are_down\neverywhere!_We_could_be\n<b>cut_off_at_any_minute!</b>"),   
        // new Narration(-31.25f, "Power_lines_are_down\neverywhere!_We_could_be\ncut_off_at_any_minute!\n"),    
        // new Narration(-29.25f, "Power_lines_are_down\neverywhere!_We_could_be\ncut_off_at_any_minute!\n<b>There's_another_group</b>\n"),     
        // new Narration(-28.25f, "Power_lines_are_down\neverywhere!_We_could_be\ncut_off_at_any_minute!\nThere's_another_group\n<b>of_spaceships_...</b>\n"),    
        // new Narration(-27.25f, "Power_lines_are_down\neverywhere!_We_could_be\ncut_off_at_any_minute!\nThere's_another_group\nof_spaceships_...\n<b>of_alien_ships_...</b>\n"),    
        // new Narration(-26.25f, "Power_lines_are_down\neverywhere!_We_could_be\ncut_off_at_any_minute!\nThere's_another_group\nof_spaceships_...\nof_alien_ships_...\n<b>They're_coming_out_of</b>\n"), 
        // new Narration(-25.25f, "Power_lines_are_down\neverywhere!_We_could_be\ncut_off_at_any_minute!\nThere's_another_group\nof_spaceships_...\nof_alien_ships_...\nThey're_coming_out_of\n<b>the_sky!</b>"), 
        // new Narration(-24.25f, "⛈\n"),
        // new Narration(-21.00f, "Click / tap\n"),
        // new Narration(-20.00f, "Click / tap\nto continue"),
        // new Narration(-01.00f, ""),
        // "Tutorial: Map Interface"
        // new Narration( 00.00f, "<b>Today_you_will_learn_to</b>"), 
        // new Narration( 01.00f, "Today_you_will_learn_to\n<b>use_the_Map_Interface</b>"),
        // new Narration( 02.25f, "Today_you_will_learn_to\nuse_the_Map_Interface\n<b>to_issue_tactical</b>"),
        // new Narration( 03.25f, "Today_you_will_learn_to\nuse_the_Map_Interface\nto_issue_tactical\n<b>commands.</b>"),
        // new Narration( 04.25f, "Today_you_will_learn_to\nuse_the_Map_Interface\nto_issue_tactical\ncommands."),
        new Narration(000.00f, "<b>⛅</b>"),
        
        new Narration(001.00f, "<b>The_map_screen_shows</b>"),
        new Narration(002.00f, "The_map_screen_shows\n<b>you_your_mission_area.</b>"),
        new Narration(005.00f, "The_map_screen_shows\nyou_your_mission_area.\n\n<b>Select_units_high-</b>"),
        new Narration(007.00f, "The_map_screen_shows\nyou_your_mission_area.\n\nSelect_units_high-\n<b>lighted_in_yellow!</b>"),
        new Narration(008.00f, "The_map_screen_shows\nyou_your_mission_area.\n\nSelect_units_high-\nlighted_in_yellow!"),
        // new Narration( 01.00f, "<b>The_Map_Screen_shows</b>"),
        // new Narration( 02.00f, "The_Map_Screen_shows\n<b>you_your_mission_area</b>"),

        new Narration(010.00f, "<b>$</b>"),
        new Narration(011.00f, "$"),
        // "Tutorial: Zoom In"
        // new Narration( 06.00f, "Today_you_will_learn_to\nuse_the_Map_Interface\nto_issue_tactical\ncommands.\n\n<b>Press_the_⇲_Zoom_key</b>\nto_zoom_in."), 
        // new Narration( 07.50f, "Today_you_will_learn_to\nuse_the_Map_Interface\nto_issue_tactical\ncommands.\n\nPress_the_⇲_Zoom_key\n<b>to_zoom_in.</b>"),
        // new Narration( 09.50f, "Press_the_⇲_Zoom_key\nto_zoom_in.\n\n<b>Click_/_tap</b>\n⇲_Zoom"),
        // new Narration( 10.50f, "Press_the_⇲_Zoom_key\nto_zoom_in.\n\nClick_/_tap\n<b>⇲_Zoom</b>"),
        // "Tutorial: Zooming In"
        new Narration(062.00f, "<b>When_the_map_is_at</b>\n"), 
        new Narration(063.00f, "When_the_map_is_at\n<b>maximum_zoom,_extra</b>\n"),
        new Narration(065.00f, "When_the_map_is_at\nmaximum_zoom,_extra\n<b>detail_is_revealed,</b>\n"),
        new Narration(066.75f, "When_the_map_is_at\nmaximum_zoom,_extra\ndetail_is_revealed,\n<b>such_as_fortifications</b>\n"),
        new Narration(068.00f, "When_the_map_is_at\nmaximum_zoom,_extra\ndetail_is_revealed,\nsuch_as_fortifications\n<b>and_individual_planes.</b>"),
        // "Tutorial: Intro"
        new Narration(0100.00f, "<b>First_off,_let's_intro-</b>"),
        new Narration(0101.00f, "First_off,_let's_intro-\n<b>duce_the_Target_Window.</b>"),
        new Narration(0104.25f, "First_off,_let's_intro-\nduce_the_Target_Window.\n\n<b>Now_the_Target_Window</b>"),
        new Narration(0105.25f, "First_off,_let's_intro-\nduce_the_Target_Window.\n\nNow_the_Target_Window\n<b>appears_whenever_you</b>"),
        new Narration(0106.50f, "First_off,_let's_intro-\nduce_the_Target_Window.\n\nNow_the_Target_Window\nappears_whenever_you\n<b>look_at_a_unit</b>"),
        new Narration(0107.50f, "First_off,_let's_intro-\nduce_the_Target_Window.\n\nNow_the_Target_Window\nappears_whenever_you\nlook_at_a_unit\n<b>with_the_crosshair.</b>"),
        new Narration(0109.25f, "First_off,_let's_intro-\nduce_the_Target_Window.\n\nNow_the_Target_Window\nappears_whenever_you\nlook_at_a_unit\nwith_the_crosshair.\n\n<b>Have_a_go_at_this_now!</b>"),
        // new Narration(0104.80f, "7 ..."),
        // new Narration(0107.80f, "7 ...\n6 ..."),
        // new Narration(0108.80f, "7 ...\n6 ...\n5 ..."),
        // new Narration(0109.80f, "7 ...\n6 ...\n5 ...\n4 ..."),
        // new Narration(0110.80f, "7 ...\n6 ...\n5 ...\n4 ...\n3 ..."),
        // new Narration(0111.80f, "7 ...\n6 ...\n5 ...\n4 ...\n3 ...\n2 ..."),
        // new Narration(0112.80f, "7 ...\n6 ...\n5 ...\n4 ...\n3 ...\n2 ...\n1 ..."),
        // new Narration(0114.80f, "Click / tap"),
        // new Narration(0115.80f, "Click / tap\nto continue ..."),
        // new Narration(0118.50f, "Today, orbitting\n"),
        // new Narration(0119.80f, "Today, orbitting\nsatellites of the"),
        // new Narration(0120.80f, "Today, orbitting\nsatellites of the\nNavy Navigation\n"),
        // new Narration(0121.50f, "Today, orbitting\nsatellites of the\nNavy Navigation\nSatellite System"),
        // new Narration(0123.50f, "Today, orbitting\nsatellites of the\nNavy Navigation\nSatellite System\nprovide around-the-\n"),
        // new Narration(0124.50f, "Today, orbitting\nsatellites of the\nNavy Navigation\nSatellite System\nprovide around-the-\nclock ultra-precise"),
        // new Narration(0126.30f, "Today, orbitting\nsatellites of the\nNavy Navigation\nSatellite System\nprovide around-the-\nclock ultra-precise\nposition fixes\n"), 
        // new Narration(0127.50f, "Today, orbitting\nsatellites of the\nNavy Navigation\nSatellite System\nprovide around-the-\nclock ultra-precise\nposition fixes\nfrom space"), 
        // new Narration(0128.50f, "Today, orbitting\nsatellites of the\nNavy Navigation\nSatellite System\nprovide around-the-\nclock ultra-precise\nposition fixes\nfrom space\nto units of\n"),
        // new Narration(0129.30f, "Today, orbitting\nsatellites of the\nNavy Navigation\nSatellite System\nprovide around-the-\nclock ultra-precise\nposition fixes\nfrom space\nto units of\nthe fleet,"),
        // new Narration(0130.00f, "Today, orbitting\nsatellites of the\nNavy Navigation\nSatellite System\nprovide around-the-\nclock ultra-precise\nposition fixes\nfrom space\nto units of\nthe fleet,\neverywhere,\n"),
        // new Narration(0131.00f, "Today, orbitting\nsatellites of the\nNavy Navigation\nSatellite System\nprovide around-the-\nclock ultra-precise\nposition fixes\nfrom space\nto units of\nthe fleet,\neverywhere,\nin any kind"),
        // new Narration(0132.30f, "Today, orbitting\nsatellites of the\nNavy Navigation\nSatellite System\nprovide around-the-\nclock ultra-precise\nposition fixes\nfrom space\nto units of\nthe fleet,\neverywhere,\nin any kind\nof weather.\n"),
        // new Narration(0134.30f, "⛈\n"),
        // new Narration(0139.00f, "Navigation\n"),
        // new Narration(0140.00f, "Navigation\nby satellite,"),
        // new Narration(0142.00f, "Navigation\nby satellite,\nhow and why\n"),
        // new Narration(0143.25f, "Navigation\nby satellite,\nhow and why\ndoes it work?"),
        // new Narration(0144.75f, "Navigation\nby satellite,\nhow and why\ndoes it work?\n\nFirst, a little\n"),
        // new Narration(0145.50f, "Navigation\nby satellite,\nhow and why\ndoes it work?\n\nFirst, a little\nastrophysics"),
        // new Narration(0146.75f, "Navigation\nby satellite,\nhow and why\ndoes it work?\n\nFirst, a little\nastrophysics\nto answer why.\n"),
        // new Narration(0148.25f, "⛈\n"),
        // new Narration(0154.50f, "Any satellite, man-\n"),
        // new Narration(0156.00f, "Any satellite, man-\nmade or not,"),
        // new Narration(0157.00f, "Any satellite, man-\nmade or not,\nremains in orbit\n"),
        // new Narration(0158.00f, "Any satellite, man-\nmade or not,\nremains in orbit\nbecause the force with"),
        // new Narration(0159.50f, "Any satellite, man-\nmade or not,\nremains in orbit\nbecause the force with\nwhich it is trying to\n"),
        // new Narration(0160.50f, "Any satellite, man-\nmade or not,\nremains in orbit\nbecause the force with\nwhich it is trying to\nfly away from Earth"),
        // new Narration(0162.25f, "Any satellite, man-\nmade or not,\nremains in orbit\nbecause the force with\nwhich it is trying to\nfly away from Earth\nis matched by the\n"),
        // new Narration(0163.25f, "Any satellite, man-\nmade or not,\nremains in orbit\nbecause the force with\nwhich it is trying to\nfly away from Earth\nis matched by the\ngravitational pull"),
        // new Narration(0164.75f, "Any satellite, man-\nmade or not,\nremains in orbit\nbecause the force with\nwhich it is trying to\nfly away from Earth\nis matched by the\ngravitational pull\nof Earth."),
        // new Narration(0166.25f, "So it continues\n"),
        // new Narration(0167.25f, "So it continues\nmoving around Earth"),
        // new Narration(0168.75f, "So it continues\nmoving around Earth\nin an orbit whose\n"),
        // new Narration(0170.25f, "So it continues\nmoving around Earth\nin an orbit whose\npath conforms very"),
        // new Narration(0171.00f, "So it continues\nmoving around Earth\nin an orbit whose\npath conforms very\nnearly to the\n"),
        // new Narration(0172.00f, "So it continues\nmoving around Earth\nin an orbit whose\npath conforms very\nnearly to the\nclassic laws of Sir"),
        // new Narration(0173.00f, "So it continues\nmoving around Earth\nin an orbit whose\npath conforms very\nnearly to the\nclassic laws of Sir\nIsaac Newton\n"),
        // new Narration(0174.00f, "So it continues\nmoving around Earth\nin an orbit whose\npath conforms very\nnearly to the\nclassic laws of Sir\nIsaac Newton\nand Johannes Kepler."),
        // new Narration(0176.80f, "Click / tap\n"),
        // new Narration(0177.80f, "Click / tap\nto continue ..."),
        // new Narration(120.00f, "Click_/_tap_anywhere\nto_skip_introduction"),
        // new Narration(124.25f, "<b>We're_inside_the</b>\n"), 
        // new Narration(125.25f, "We're_inside_the\n<b>operation_center:</b>"), 
        // new Narration(127.25f, "We're_inside_the\noperation_center:\n<b>command_post_for_the</b>\n"), 
        // new Narration(128.25f, "We're_inside_the\noperation_center:\ncommand_post_for_the\n<b>operations_duty_officer.</b>"), 
        // new Narration(133.00f, "<b>Mission_of_the_group:</b>\n"), 
        // new Narration(135.00f, "around_the_clock,\n"), 
        // new Narration(136.00f, "around_the_clock,\nseven_days_a_week,"), 
        // new Narration(137.50f, "is_to_make_sure_that\n"), 
        // new Narration(138.50f, "is_to_make_sure_that\neach_navigation"), 
        // new Narration(139.50f, "satellite_always_has\n"), 
        // new Narration(141.50f, "satellite_always_has\ncorrect,_up-to-date"), 
        // new Narration(142.50f, "information_stored_in\n"), 
        // new Narration(144.50f, "information_stored_in\nits_memory unit."), 
        // new Narration(148.00f, "An informative array\n"), 
        // new Narration(149.25f, "An informative array\nof actuated displays"), 
        // new Narration(151.25f, "shows the performance,\n"), 
        // new Narration(153.25f, "shows the performance,\nmemory,"), 
        // new Narration(154.00f, "and injection status\n"), 
        // new Narration(156.00f, "and injection status\nand helps the duty"), 
        // new Narration(157.00f, "officer coordinate\n"), 
        // new Narration(158.50f, "officer coordinate\nall network operations"), 
        // new Narration(160.50f, "that keep the satellites\n"), 
        // new Narration(161.50f, "that keep the satellites\nbroadcasting navigation"), 
        // new Narration(163.25f, "data.\n"), 
        // new Narration(165.25f, "Click / tap\nto continue"), 
        // // "Tutorial: Welcome"
        // new Narration(180.00f, "Welcome to the U.S.\n"), 
        // new Narration(181.00f, "Welcome to the U.S.\nNaval Academy"),
        // new Narration(182.50f, "Aerial Ordinance\n"), 
        // new Narration(183.50f, "Aerial Ordinance\nTutorial"), 
        // new Narration(185.00f, "Here, you will learn\n"),
        // new Narration(186.25f, "Here, you will learn\nthe main ways"),
        // new Narration(187.00f, "of attacking targets\n"),
        // new Narration(188.50f, "of attacking targets\nfrom the air."),
        // new Narration(189.50f, "So, be sure to pay\n"),
        // new Narration(190.50f, "So, be sure to pay\nattention!"),
        // // "Tutorial: Printer"
        // new Narration(192.00f, "Aim the crosshair using\n"),
        // new Narration(194.00f, "Aim the crosshair using\nthe mouse"),
        // new Narration(195.00f, "or joystick.\n"),
        // new Narration(196.00f, "When you're on target\n"),
        // new Narration(197.00f, "When you're on target\nthe crosshair"),
        // new Narration(198.00f, "alters shape to\n"),
        // new Narration(199.00f, "alters shape to\nindicate a"),
        // new Narration(200.00f, "lock-on.\n"),
        // new Narration(202.00f, "Click / tap\n"),
        // new Narration(203.00f, "Click / tap\n\"Printer\""),
        // // "Tutorial: Print"
        // new Narration(240.00f, "Good, good.\n"),
        // new Narration(241.00f, "You can also use\n"),
        // new Narration(241.75f, "You can also use\nthe crosshair"),
        // new Narration(243.00f, "to issue orders to your\n"),
        // new Narration(244.00f, "to issue orders to your\ncurrently seleted unit."),
        // new Narration(246.00f, "Click / tap\n"),
        // new Narration(247.00f, "Click / tap\n\"Print\""),
        // // "Tutorial: Pan"
        // new Narration(300.00f, "Your target will be\n"),
        // new Narration(301.00f, "Your target will be\nthat cargo hulk there"),
        // new Narration(302.00f, "While she moves into\n"),
        // new Narration(303.00f, "While she moves into\nposition"),
        // new Narration(304.00f, "Let's explain how\n"), 
        // new Narration(305.00f, "Let's explain how\nartillery works."),

        // // Looking Around
        // new Narration(180.00f, "First_off,_try\n"),
        // new Narration(181.00f, "First_off,_try\nlooking_around!"),
        // new Narration(182.00f, "First_off,_try\nlooking_around!\n\n360°_awareness\n"),
        // new Narration(183.00f, "First_off,_try\nlooking_around!\n\n360°_awareness\nis_needed_for"),
        // new Narration(184.00f, "First_off,_try\nlooking_around!\n\n360°_awareness\nis_needed_for\ndog-fighting!"),
        // // Map Screen
        // new Narration(200.00f, "The_map_screen_shows\n"),
        // new Narration(201.00f, "The_map_screen_shows\nyou_your_mission_area,\n"),
        // new Narration(202.00f, "The_map_screen_shows\nyou_your_mission_area,\nall_friendly_units,"),
        // new Narration(203.00f, "The_map_screen_shows\nyou_your_mission_area,\nall_friendly_units,\nand_any_detected\n"),
        // new Narration(204.00f, "The_map_screen_shows\nyou_your_mission_area,\nall_friendly_units,\nand_any_detected\nenemy_units."),
        new Narration(205.00f, "<b>Great!</b>"),//"The_map_screen_shows\nyou_your_mission_area,\nall_friendly_units,\nand_any_detected\nenemy_units.\n\nSelect_units_high-"),
        new Narration(206.00f, "Great!\n\n<b>Time_to_get_this_baby</b>"),
        new Narration(207.00f, "Great!\n\nTime_to_get_this_baby\n<b>airborne!</b>"),
        new Narration(208.00f, "Great!\n\nTime_to_get_this_baby\nairborne!\n\n<b>Set_your_throttle_to</b>"),
        new Narration(209.00f, "Great!\n\nTime_to_get_this_baby\nairborne!\n\nSet_your_throttle_to\n<b>maximum</b>!"),
        new Narration(210.00f, "Great!\n\nTime_to_get_this_baby\nairborne!\n\nSet_your_throttle_to\nmaximum!"),
        new Narration(212.00f, "$"),
        // new Narration(215.00f, "Great!\n\nTime_to_get_this_baby\nairborne!\n\nSet_your_throttle_to\nmaximum!\n\nHint:_Fly_forward_to_take_off"),
        // new Narration(216.00f, "Great!\n\nTime_to_get_this_baby\nairborne!\n\nSet_your_throttle_to\nmaximum!\n\nHint:_Fly_forward_to\ntake_off_of_Mars!"),
        //"The_map_screen_shows\nyou_your_mission_area,\nall_friendly_units,\nand_any_detected\nenemy_units.\n\nSelect_units_high-\nlighted_in_yellow!"),
        // new Narration(311.00f, "Click / tap\n"),
        // new Narration(312.00f, "Click / tap\n\"Pan\" or Drag"),
        // // "Tutorial: Target Button"
        // new Narration(360.00f, "You can also use the\n"),
        // new Narration(361.50f, "You can also use the\nTarget Button"),
        // new Narration(363.00f, "to cycle targets\n"),
        // new Narration(364.00f, "to cycle targets\nbetween all"),
        // new Narration(365.00f, "detected enemy units.\n"),
        // new Narration(367.00f, "Click / tap\n"),
        // new Narration(368.00f, "Click / tap\n\"Target\""),
        // // "Tutorial: Target 1"
        // new Narration(420.00f, "Good.\n"),
        // new Narration(422.00f, "Click / tap\n"),
        // new Narration(423.00f, "Click / tap\n\"Target\""),
        // // "Tutorial: Target 2"
        // new Narration(480.00f, "Good.\n"),
        // new Narration(482.00f, "Click / tap\n"),
        // new Narration(483.00f, "Click / tap\n\"Target\""),
        // // "Tutorial: Target 3"
        // new Narration(540.00f, "Good!\n"),
        // new Narration(542.00f, "Click / tap\n"),
        // new Narration(543.00f, "Click / tap\n\"Target\""),
        // // "Tutorial: Use Weapon"
        // new Narration(600.00f, "Okay cadet, strafe\n"),
        // new Narration(601.00f, "Okay cadet, strafe\nthat cargo ship"),
        // new Narration(602.50f, "until she goes down!\n"),
        // new Narration(605.00f, "Press the \"Use Weapon\"\n"),
        // new Narration(606.50f, "Press the \"Use Weapon\"\ncontrol to fire!"),
        // new Narration(608.50f, "Click / tap\n"),
        // new Narration(609.50f, "Click / tap\n\"Use Weapon\""),
        // // "Tutorial: Shoot 1"
        // new Narration(660.00f, "Although torpedos are\n"),
        // new Narration(661.50f, "Although torpedos are\npowerful, they're slow!"),
        // new Narration(663.75f, "Firing a spread makes\n"),
        // new Narration(664.75f, "Firing a spread makes\nit harder"),
        // new Narration(665.75f, "for your enemy to\n"),
        // new Narration(666.75f, "for your enemy to\navoid them."),
        // new Narration(668.75f, "Click / tap\n"),
        // new Narration(669.75f, "Click / tap\n\"Use Weapon\""),
        // // "Tutorial: Destroy 1"
        // new Narration(780.00f, "Hah, great stuff!\n"),
        // new Narration(781.50f, "Hah, great stuff!\nLet's move on."),
        // // "Tutorial: Joystick"
        // new Narration(783.00f, "Now it's time to get\n"),
        // new Narration(784.00f, "Now it's time to get\nthis old girl moving!"),
        // new Narration(786.00f, "Set your throttle\n"),
        // new Narration(787.00f, "Set your throttle\nto maximum!"),
        // new Narration(789.00f, "Click / drag\n"),
        // new Narration(790.00f, "Click / drag\n\"Joystick\""),
        // // "Tutorial: Binocular"
        // new Narration(842.00f, "You can also shoot from\n"),
        // new Narration(844.00f, "You can also shoot from\nBinocular View"),
        // new Narration(845.00f, "which can help you aim\n"),
        // new Narration(846.00f, "which can help you aim\nmore accurately."),
        // new Narration(848.00f, "Click / tap\n"),
        // new Narration(848.00f, "Click / tap\n\"Binocular\""),
        // // "Tutorial: Binocular"
        // new Narration(880.00f, "Good!\n"),
        // new Narration(882.00f, "\n"),
        // // "Tutorial: Destroy 2"
        // new Narration(930.00f, "That was right on\n"),
        // new Narration(931.00f, "That was right on\nthe money!"),
        // new Narration(933.00f, "Great stuff!\n"),
        // new Narration(935.00f, ""),
        // // "Tutorial: Destroy 3"
        // new Narration(960.00f, "Excellence work!\n"),
        // new Narration(961.50f, "You have now completed\n"),
        // new Narration(962.50f, "You have now completed\nthe tutorial!"),
        // new Narration(964.00f, "I hope you never have\n"),
        // new Narration(965.00f, "I hope you never have\ncause to use"),
        // new Narration(966.50f, "the knowledge you have\n"),
        // new Narration(967.50f, "the knowledge you have\njust acquired."),
        // new Narration(969.00f, "That is all for today!\n"),
        // new Narration(970.50f, "That is all for today!\nDismissed!"),
        // new Narration(972.50f, ""),
        // new Narration(999.00f, "Click / tap\n"),
        new Narration(1000.0f, "⛈"),
        new Narration(1001.0f, "⛅"),
        // CAMPAIGN LEVEL 1: INTRODUCTION
        new Narration(2000.00f, "⛈\n"),
        new Narration(2100.00f, "Professor_Pierson_has"),
        new Narration(2101.00f, "Professor_Pierson_has\nbeen_located_where_he"),
        new Narration(2102.00f, "Professor_Pierson_has\nbeen_located_where_he\nhas_established_an\n"),
        new Narration(2103.00f, "Professor_Pierson_has\nbeen_located_where_he\nhas_established_an\nemergency_observation"),
        new Narration(2104.00f, "Professor_Pierson_has\nbeen_located_where_he\nhas_established_an\nemergency_observation\npost!"),
        new Narration(2105.00f, "Professor_Pierson_has\nbeen_located_where_he\nhas_established_an\nemergency_observation\npost!\n\nAs_a_scientist,_he"),
        new Narration(2106.00f, "Professor_Pierson_has\nbeen_located_where_he\nhas_established_an\nemergency_observation\npost!\n\nAs_a_scientist,_he\nwill_give_his_assess"),
        // new Narration(2001.25f, "Captain!\nThe radar scanner"),
        // new Narration(2002.25f, "indicates two shining\n"),
        // new Narration(2003.25f, "indicates two shining\npoints!"),
        // new Narration(2004.25f, "They're approaching\n"),
        // new Narration(2005.00f, "They're approaching\nrapidly!"),
        // new Narration(2007.50f, "⛈\n"),
        // new Narration(2010.25f, "All crews to your\n"),
        // new Narration(2011.25f, "All crews to your\nstations!"),
        // new Narration(2013.25f, "⛈\n"),
        // new Narration(2015.25f, "Click / tap\n"),
        // new Narration(2016.25f, "Click / tap\n⇲ Zoom"),
        // new Narration(2100.00f, "Click / tap\nto continue."),
        // new Narration(2102.00f, "⛅\n"),        
        // new Narration(2104.00f, "\n"),        
        // new Narration(2106.50f, "Establish contact\n"),
        // new Narration(2107.50f, "Establish contact\nwith Base Ὠρίων."),
        // new Narration(2109.25f, "Tell Commander\n"),
        // new Narration(2110.00f, "Tell Commander\nArmstrong it's urgent!"),
        // new Narration(2112.50f, "Spaceship MK-31 asks\n"),
        // new Narration(2113.75f, "Spaceship MK-31 asks\nto speak with"),
        // new Narration(2114.75f, "Commander Armstrong.\n"),
        // new Narration(2116.00f, "Commander Armstrong.\nIt's urgent!"),
        // new Narration(2118.00f, "Commander Armstrong's\n"),
        // new Narration(2119.00f, "Commander Armstrong's\ncoming ..."),
        // new Narration(2120.50f, "What's happening,\n"),
        // new Narration(2121.00f, "What's happening,\nCaptain?"),
        // new Narration(2122.00f, "Our radar is picking\n"),
        // new Narration(2123.25f, "Our radar is picking\nup two strange"),
        // new Narration(2124.00f, "metallic bodies.\n"),
        // new Narration(2124.75f, "I presume they're\n"),
        // new Narration(2125.75f, "I presume they're\nspaceships."),
        // new Narration(2126.50f, "Spaceships in that\n"),
        // new Narration(2127.25f, "Spaceships in that\npart of space?"),
        // new Narration(2128.25f, "They could be\n"),
        // new Narration(2129.00f, "They could be\nasteroids!"),
        // new Narration(2130.00f, "They've got to be\n"),
        // new Narration(2130.75f, "They've got to be\nasteroids!"),
        // new Narration(2133.00f, "No asteroids could\n"),
        // new Narration(2133.75f, "No asteroids could\nvary their speed"),
        // new Narration(2134.50f, "like that!\n"),
        // new Narration(2136.00f, "They're being\n"),
        // new Narration(2136.50f, "They're being\ncontrolled somehow!"),
        // new Narration(2137.75f, "Captain, try to find\n"),
        // new Narration(2138.75f, "Captain, try to find\nout what's happening!"),
        // new Narration(2139.75f, "But remember they're\n"),
        // new Narration(2140.75f, "But remember they're\nnot terrestrial"),
        // new Narration(2141.75f, "spaceships!\n"),
        // new Narration(2142.75f, "That is if they are\n"),
        // new Narration(2143.75f, "That is if they are\nspaceships!"),
        // new Narration(2144.50f, "There is a way to get\n"),
        // new Narration(2145.50f, "There is a way to get\nan answer to that."),
        // new Narration(2146.50f, "Activate the scanners!\n"),
        // new Narration(2150.25f, "Activate the scanners!\nExecuted."),
        // new Narration(2151.50f, "Executed.\n"),
        // new Narration(2152.50f, "Executed.\nExecuted."),
        // new Narration(2154.50f, "⛅\n"),
        // new Narration(2155.50f, "\n"),
        // new Narration(2156.25f, "The two objects are\n"),
        // new Narration(2157.25f, "The two objects are\nnow on our cameras!"),
        // new Narration(2159.00f, "They'll be on your\n"),
        // new Narration(2159.50f, "They'll be on your\nbase scanner"),
        // new Narration(2160.50f, "momentarily.\n"),
        // new Narration(2161.50f, "Transfer everything to\n"),
        // new Narration(2162.25f, "Transfer everything to\nBase Ὠρίων."),
        // new Narration(2164.00f, "⛅\n"),
        // new Narration(2166.00f, "\n"),
        // CAMPAIGN LEVEL 1: SELECT ALIEN
        new Narration(2198.75f, "Armstrong calling ...\n"),
        // new Narration(2200.50f, "⛅\n"),
        // new Narration(2201.00f, "\n"),
        // new Narration(2201.75f, "And they're really\n"),
        // new Narration(2202.25f, "And they're really\nspaceships!"),
        // new Narration(2203.50f, "There just isn't any\n"),
        // new Narration(2204.75f, "There just isn't any\nother possibility."),
        // new Narration(2206.75f, "Establish contact!\n"),
        // new Narration(2208.00f, "Establish contact!\nNegative contact."),
        // new Narration(2210.00f, "Hamilton, try again!\n"),
        // new Narration(2211.75f, "Hamilton, try again!\nTry with all possible"),
        // new Narration(2212.75f, "ways of communications.\n"),
        // new Narration(2214.00f, "ways of communications.\nTry with a radio too!"),
        // new Narration(2216.50f, "هالة          \n"),        
        // new Narration(2217.75f, "هالة interrupt\n"),        
        // new Narration(2218.50f, "هالة interrupt\ncontact with base."),
        // new Narration(2219.50f, "⛈\n"),
        // new Narration(2221.00f, "\n"),
        // new Narration(2221.50f, "Мила            \n"),
        // new Narration(2222.75f, "Мила analyze the\n"),
        // new Narration(2223.25f, "Мила analyze the\nspaceship on our"),
        // new Narration(2224.00f, "computer.\n"),
        // new Narration(2225.00f, "Ask data about the\n"),
        // new Narration(2225.75f, "Ask data about the\ncrew and their weapons"),
        // new Narration(2227.50f, "in terms of absolute\n"),
        // new Narration(2228.00f, "in terms of absolute\nprobability."),
        // new Narration(2229.00f, "⛅\n"),
        // new Narration(2230.00f, "\n"),
        // new Narration(2230.50f, "Unmanned spaceships\n"),
        // new Narration(2233.00f, "Unmanned spaceships\narmed with long-range"),
        // new Narration(2235.25f, "disintegrators!\n"),
        // new Narration(2237.00f, "disintegrators!\n70% chance."),
        // new Narration(2239.75f, "Battlespeed!\n"),
        // new Narration(2241.75f, "Battlespeed!\nBattlespeed."),
        // new Narration(2242.50f, "Activate disintegrators!\n"),
        // new Narration(2244.00f, "Activate disintegrators!\nCoordinate direction rays!"),
        // new Narration(2246.00f, "Activate laser!\n"),
        // new Narration(2248.00f, "⛅\n"),
        // new Narration(2249.00f, "\n"),
        // new Narration(2249.50f, "Best range ...\n"),
        // new Narration(2250.50f, "Best range ...\n50 miles."),
        // new Narration(2251.75f, "Approaching range ...\n"),
        // new Narration(2254.75f, "\n"),
        // new Narration(2256.50f, "\n"),
        // new Narration(2257.50f, "\n"),
        // new Narration(2258.50f, "\n"),
        // new Narration(2261.25f, "\n"),
        // new Narration(2262.50f, "\n"),
        // new Narration(2264.00f, "\n"),
        // new Narration(2265.00f, "\n"),
        // new Narration(2266.75f, "\n"),
        // new Narration(2267.75f, "\n"),
        // new Narration(2269.00f, "\n"),
        // new Narration(2270.00f, "\n"),
        // new Narration(2272.00f, "\n"),
        // new Narration(2274.00f, "\n"),
        // new Narration(2276.00f, "⛈\n"),
        // new Narration(2278.00f, "\n"),
        // CAMPAIGN LEVEL 1: DESTROY ALIEN
        // new Narration(2300.00f, "\n"),
        // new Narration(2302.00f, "What's happening?\n"),
        // new Narration(2303.00f, "Reactivate contact\n"),
        // new Narration(2304.00f, "Reactivate contact\nat once!"),
        // new Narration(2305.00f, "Our equipment seems to\n"),
        // new Narration(2306.00f, "Our equipment seems to\nbe working perfectly"),
        // new Narration(2306.75f, "Commander, they must've\n"),
        // new Narration(2307.50f, "Commander, they must've\nstopped transmitting."),
        // new Narration(2309.00f, "Commander, the news\n"),
        // new Narration(2310.00f, "Commander, the news\npapermen are in the"),
        // new Narration(2310.85f, "press hall. They\n"),
        // new Narration(2312.00f, "press hall. They\nfollowed everything."),
        // new Narration(2314.00f, "\n"),
        // new Narration(2315.50f, "⛈\n"),
        // new Narration(2320.00f, "Commander!\n"),
        // new Narration(2320.50f, "Commander!\nThere he is!"),
        // new Narration(2321.25f, "Commander, please give\n"),
        // new Narration(2322.50f, "Commander, please give\nus some straight"),
        // new Narration(2323.00f, "answers on this!\n"),
        // new Narration(2323.85f, "answers on this!\nNow!"),
        // new Narration(2324.25f, "We must get an answer\n"),
        // new Narration(2325.00f, "We must get an answer\nto this!"),
        // new Narration(2326.00f, "Commander! We've got\n"),
        // new Narration(2327.00f, "Commander! We've got\na lot of questions"),
        // new Narration(2327.65f, "to ask!\n"),
        // new Narration(2328.25f, "to ask!\nAre there plans to"),
        // new Narration(2329.00f, "answer this attack\n"),
        // new Narration(2329.75f, "answer this attack\nagainst the Earth?"),
        // new Narration(2330.75f, "What are you talking\n"),
        // new Narration(2331.50f, "What are you talking\nabout?"),
        // new Narration(2332.00f, "There's been no\n"),
        // new Narration(2332.50f, "There's been no\nattack against our"),
        // new Narration(2333.00f, "planet.\n"),
        // new Narration(2334.00f, "planet.\nWe've all seen the"),
        // new Narration(2335.00f, "spaceship.\n"),
        // new Narration(2336.00f, "spaceship.\nDo you think MK-31"),
        // new Narration(2337.75f, "has been hit as part\n"),
        // new Narration(2338.75f, "has been hit as part\nof some weird game?"),
        // new Narration(2339.50f, "Will you please stop\n"),
        // new Narration(2340.25f, "Will you please stop\nmisrepresenting facts."),
        // new Narration(2341.75f, "A space skirmish\n"),
        // new Narration(2342.75f, "A space skirmish\ndoes not mean the"),
        // new Narration(2343.25f, "Earth is in peril\n"),
        // new Narration(2344.25f, "Earth is in peril\nand that we've been"),
        // new Narration(2344.75f, "attacked!\n"),
        // new Narration(2345.00f, "attacked!\nThe public must know"),
        // new Narration(2345.75f, "about that ship!\n"),
        // new Narration(2346.50f, "about that ship!\nWe may be facing"),
        // new Narration(2347.25f, "the vangaurd of a\n"),
        // new Narration(2348.00f, "the vangaurd of a\nwhole fleet!"),
        // new Narration(2349.50f, "Damn, that's enough!\n"),
        // new Narration(2350.50f, "Damn, that's enough!\nGet out of here!"),
        // new Narration(2351.25f, "I won't answer any\n"),
        // new Narration(2351.85f, "I won't answer any\nmore questions!"),
        // new Narration(2353.85f, "⛈\n"),
        // new Narration(2357.00f, "The Earth is in danger!\n"),
        // new Narration(2358.50f, "The Earth is in danger!\nThe Earth is in"),
        // new Narration(2359.50f, "desperate, desperate\n"),
        // new Narration(2361.00f, "desperate, desperate\ndanger!"),
        // new Narration(2363.00f, "It is very urgent\n"),
        // new Narration(2364.00f, "It is very urgent\nthat we prepare"),
        // new Narration(2365.00f, "ourselves!\n"),
        // new Narration(2365.25f, "ourselves!\nAliens are attacking"),
        // new Narration(2366.00f, "Earth!\n"),
        // new Narration(2367.00f, "Earth!\nThe vangaurd of a"),
        // new Narration(2367.65f, "fleet!\n"),
        // new Narration(2368.50f, "fleet!\nAll bases prepare"),
        // new Narration(2369.75f, "defenses!\n"),
        // new Narration(2370.85f, "defenses!\nEarth can be"),
        // new Narration(2372.00f, "destroyed like MK-31.\n"),
        // new Narration(2372.75f, "destroyed like MK-31.\nSpaceship enters our"),
        // new Narration(2373.75f, "Solar System.\n"),
        // new Narration(2374.25f, "Solar System.\nRepeat, spaceship"),
        // new Narration(2375.50f, "enters our\n"),
        // new Narration(2376.00f, "enters our\nSolar System."),
        // new Narration(2376.50f, "Lo Base Ὠρίων's monitor\n"),
        // new Narration(2378.50f, "Lo Base Ὠρίων's monitor\nWe have seen the"),
        // new Narration(2379.50f, "beginning of the end!\n"),
        // new Narration(2381.50f, "⛈\n"),
        // new Narration(2382.50f, "\n"),
        // CAMPAIGN LEVEL 2: PEARL HARBOR
        // new Narration(2500.00f, "\n"),
        // new Narration(2500.25f, "General quarters!\n"),
        // new Narration(2501.00f, "General quarters!\nGeneral quarters!"),
        // new Narration(2502.00f, "Enemy aircraft\n"),
        // new Narration(2502.65f, "Enemy aircraft\nincoming!"),
        // new Narration(2503.50f, "All combat vessels:\n"),
        // new Narration(2504.50f, "All combat vessels:\nevacuate the harbor!"),
        // new Narration(2505.75f, "Repeat:\n"),
        // new Narration(2506.15f, "Repeat:\nall combat vessels:"),
        // new Narration(2507.15f, "evacuate the harbor!\n"),
        // new Narration(2508.50f, "⛈\n"),
        // CAMPAIGN LEVEL 2: INTRODUCTION
        // CAMPAIGN LEVEL 2: DESTROY ALIEN
        // new Narration(2800.00f, "\n"),
        // new Narration(2801.00f, "⛅\n"),
        // new Narration(2803.25f, "⛈\n"),
        // new Narration(2805.00f, "Commander!\n"),
        // new Narration(2805.50f, "Commander!\nThere he is!"),
        // new Narration(2806.00f, "Commander!\n"),
        // new Narration(2806.75f, "Commander!\nIf you could"),
        // new Narration(2807.25f, "Pay attention please!\n"),
        // new Narration(2809.00f, "Are you withholding\n"),
        // new Narration(2810.00f, "Are you withholding\nanything?"),
        // new Narration(2810.50f, "Did you locate\n"),
        // new Narration(2811.25f, "Did you locate\nthe ship, Captain?"),
        // new Narration(2812.25f, "Uh, I don't know\n"),
        // new Narration(2813.25f, "Uh, I don't know\nthat, sir."),
        // new Narration(2813.75f, "Did you?!\n"),
        // new Narration(2814.25f, "I believe so.\n"),
        // new Narration(2815.25f, "I believe so.\nThe enemy spaceship"),
        // new Narration(2816.50f, "should be in the Artic.\n"),
        // new Narration(2817.75f, "should be in the Artic.\nThe situation is under"),
        // new Narration(2819.25f, "control ... I'm sorry\n"),
        // new Narration(2820.85f, "control ... I'm sorry\nfor the headlines you"),
        // new Narration(2821.75f, "had in mind but Earth\n"),
        // new Narration(2823.50f, "had in mind but Earth\nis not in any danger."),
        // new Narration(2825.00f, "⛈\n"),
        // new Narration(2831.25f, "Earth is in danger!\n"),
        // new Narration(2832.65f, "Earth is in danger!\nDidn't buy it ..."),
        // new Narration(2833.65f, "No ...\n"),
        // new Narration(2837.00f, "⛈\n"),

        // new Narration(3001.00f, "⛅"),

        // CAMPAIGN LEVEL 3: MIDWAY
        // new Narration(3000.00f, "\n"),
        // new Narration(3000.25f, "Stand by!\n"),
        // new Narration(3001.00f, "Stand by!\nStand by!"),
        // new Narration(3002.00f, "Enemy aircraft\n"),
        // new Narration(3002.65f, "Enemy aircraft\ninbound!"),
        // new Narration(3008.50f, "\n"),

        // new Narration(9999.99f, "\n"),
        // new Narration(19999.9f, "\n")
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
    float NarrationTimer = 0;//-20f; //-60;
    int NarrationIndex = 0;
    public AudioClip CampaignPearl, CampaignPearlMusic, CampaignMidway, CampaignMidwayMusic, MultiplayerSelect, MultiplayerSelectMusic;
    public AudioClip NarratorSwitchToMap, NarratorZoomInMap, NarratorZoomInDetails, NarratorAimTheCrosshair, NarratorWelcome;
    public AudioClip TutorialOpening, TutorialLockOn, TutorialPrint, TutorialPrinted, TutorialComponentsIcons, TutorialLook, TutorialUseWeapon, TutorialBinocular, TutorialHitTarget, TutorialRightOn, TutorialAttackTarget, TutorialCycle, TutorialHulkDestroyed, TutorialTorpedoFired, TutorialCannonFired, TutorialSensorFired, TutorialTarget;
    public GameObject CampaignNewtonsLaws, CampaignDopplerShift, CampaignDopplerEffect, CampaignPlanksLaw, CampaignHawkingRadiation, CampaignMoracevsParadox, CampaignDeBroglieTheory, CampaignFermiParadox, CampaignPascalsWager;
    public AudioClip HookNarration, SplashScreenNarration, CampaignRadioDaysNarration, CampaignNewtonsLawsNarration, CampaignTheAtomNarration, CampaignDopplerShiftNarration, CampaignTheElectronNarration, CampaignDopplerEffectNarration, CampaignModernWarNarration, CampaignPlanksLawNarration, CampaignTelevisionNarration, CampaignHawkingRadiationNarration, CampaignVideotapeRecordsNarration, CampaignMoracevsParadoxNarration, CampaignElectronicMusicNarration, CampaignDeBroglieTheoryNarration, CampaignRadioIsotopesNarration, CampaignFermiParadoxNarration, CampaignHardnessTestNarration, CampaignPascalsWagerNarration, CampaignConclusionNarration, CampaignCreditsNarration;
    public GameObject Content, InterpreterPanel, MapPanel, SubtitlesShadow, Subtitles; 
    public AudioClip TutorialIntro, TutorialLookAround, TutorialMapInterface, TutorialMapScreen, TutorialIssueOrders, TutorialTargetWindow, TutorialTargetWindowHelp, TutorialTargetWindowSelected, TutorialGood, TutorialGood2, TutorialGood3, TutorialTry, TutorialBetter, TutorialCancel, TutorialOther, TutorialMusic, TutorialComponents, TutorialGetMoving, TutorialThrottle, TutorialDogfight, TutorialOutro, TutorialLeftWindow, TutorialRightWindow, TutorialCursor, TutorialSelect;
    public AudioClip CannonFire, ThrusterThrottle, SonarScan, TorpedoFact, ProcessorPing, GimbalRotate, TorpedoLaunch;
    public AudioClip ThemeSong, Click, Click2;
    public AudioClip SoundBack, SoundClick, SoundError, SoundOnMouse, SoundStart, SoundToggle, SoundProcessor, SoundGimbal, SoundCannon1, SoundCannon2, SoundCannon3, SoundRadar, SoundThruster, SoundBooster, SoundTorpedo1, SoundTorpedo2, SoundWarning, SoundWarningOver;
    public GameObject Overlay, OverlayZoomIn, OverlayZoomOut;
    public GameObject Example;
    public GameObject PrinterPrint, PrinterRight, PrinterLeft;
    private string command = "";
    private string history = "";
    public StructureController Ship, Enemy;
    public GameObject volume_slider;
    public GameObject InterpreterZoomIn, InterpreterZoomOut;
    public string start_text = "$"; 
    public OverlayInteractor OverlayInteractor;
    public GameObject ClickableText;
    Text TabToggle;
    GameObject MapScreenPanOverlay, CycleToggle, BinocularToggle;
    public Text InputField;
    public Text Timer, TimerShadow, SplitTimer, SplitTimerShadow;
    GameObject camera;
    public List<GameObject> ButtonsCache = new List<GameObject>();
    int cache_size = 125;
    public AudioClip clip_queue;
    public GameObject CannonL, Processor, Bulkhead, BoosterR, ThrusterL, BoosterL, Thruster, ThrusterR, CannonR, SensorL, SensorR, Printer;
    void Start()
    {
        InputJoystick.SetActive(false);
        InputUseWeapon.SetActive(false);
        PrinterPrint = GameObject.Find("InputPrinterPrint");
        PrinterRight = GameObject.Find("InputPrinterRight");
        PrinterLeft = GameObject.Find("InputPrinterLeft");
        OverlayZoomIn = GameObject.Find("OverlayZoomIn");
        OverlayZoomOut = GameObject.Find("OverlayZoomOut");
        TabToggle = GameObject.Find("TabToggle")?.GetComponent<Text>();
        SplashScreen.SetActive(true);
        // Subtitles = GameObject.Find("Subtitles");
        // SubtitlesShadow = GameObject.Find("SubtitlesShadow");
        // SubtitlesShadow.SetActive(true);
        // Subtitles.SetActive(true);
        MapSubtitles("Click / tap\nto continue");
        camera = GameObject.Find("Main Camera");
        volume_slider = GameObject.Find("VolumeSlider");
        InterpreterZoomIn = GameObject.Find("InterpreterZoomIn");
        InterpreterZoomOut = GameObject.Find("InterpreterZoomOut");
        for (int i = 0; i < cache_size; i++) {
            ButtonsCache.Add(Instantiate(ClickableText, Content.transform) as GameObject);
        } 
        OverlayInteractor = GameObject.Find("OverlayBorder")?.GetComponent<OverlayInteractor>();
        MapScreenPanOverlay = GameObject.Find("MapScreenPanOverlay");
        RenderText("$");
        // PlayVideo("SplashScreen");
        // PlayVideo("WarOfTheWorldsStinger");
        LoadingScreen.SetActive(false);
        ResetVideo();
        OnMapView();
        // OverlayZoomIn.SetActive(false);
        PrinterLeft.SetActive(false);
        PrinterRight.SetActive(false);
        PrinterPrint.SetActive(false);
        // Printer.SetActive(false);
        BinocularToggle = GameObject.Find("BinocularToggle");
        CycleToggle = GameObject.Find("CycleToggle");
        BinocularToggle.SetActive(false);
        CycleToggle.SetActive(false);
        Map = GameObject.Find("Map")?.GetComponent<AbstractMapController>();
        // Map.SetMars();
    }
    public string GetBinocular() {
        return BinocularToggle.GetComponentsInChildren<Text>()[0].text;
    }
    public void SetBinocular(string text) {
        BinocularToggle.GetComponentsInChildren<Text>()[0].text = text;
        if (NarrationIndex > 1 && NarrationIndex < 141 + 26 && text == "⛯") {
            NarrationTimer = 880;
            NarrationIndex = 141 + 26;
        }
    }
    public void HitSfx() {
        if (tutorial_clip_index == 6) {
            tutorial_clip_index = 7;
            tutorial_timer = 0;
            PlayAudio(TutorialHitTarget);
        }
    }
    // public void PrinterLeftFx() {
    //     if (BoosterR.activeSelf)
    //     {
    //         Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){new ClassObj("◌")});
    //         BoosterL.SetActive(false);
    //         BoosterR.SetActive(false);
    //         SensorL.SetActive(true);
    //         SensorR.SetActive(true);
    //         ThrusterL.SetActive(true);
    //         ThrusterR.SetActive(true);
    //         Thruster.SetActive(false);
    //     } else if (CannonR.activeSelf) {
    //         Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){new ClassObj("◎")});
    //         CannonL.SetActive(false);
    //         CannonR.SetActive(false);
    //         BoosterL.SetActive(true);
    //         BoosterR.SetActive(true);
    //         Thruster.SetActive(true);
    //         ThrusterL.SetActive(false);
    //         ThrusterR.SetActive(false);
    //     } else {
    //         Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){new ClassObj("◍")});
    //         SensorL.SetActive(false);
    //         SensorR.SetActive(false);
    //         CannonL.SetActive(true);
    //         CannonR.SetActive(true);
    //     }

    // }
    // public void PrinterRightFx() {
    //     if (BoosterR.activeSelf)
    //     {
    //         Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){new ClassObj("◍")});
    //         BoosterL.SetActive(false);
    //         BoosterR.SetActive(false);
    //         CannonL.SetActive(true);
    //         CannonR.SetActive(true);
    //         ThrusterL.SetActive(true);
    //         ThrusterR.SetActive(true);
    //         Thruster.SetActive(false);
    //     } else if (CannonR.activeSelf) {
    //         Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){new ClassObj("◌")});
    //         CannonL.SetActive(false);
    //         CannonR.SetActive(false);
    //         SensorL.SetActive(true);
    //         SensorR.SetActive(true);
    //     } else {
    //         Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){new ClassObj("◎")});
    //         SensorL.SetActive(false);
    //         SensorR.SetActive(false);
    //         BoosterL.SetActive(true);
    //         BoosterR.SetActive(true);
    //         Thruster.SetActive(true);
    //         ThrusterL.SetActive(false);
    //         ThrusterR.SetActive(false);
    //     }
    // }
    // public void InputWFx() {
    //     if (Thruster.activeSelf) {
    //         GameObject.Find("Thruster").GetComponent<ComponentController>().Action(25);
    //     } else {
    //         GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(25);
    //         GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(25);
    //     }
    // }
    // public void InputDFx() {
    //     if (Thruster.activeSelf) {
    //         GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(25);
    //     } else {
    //         GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(25);
    //         GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-25);
    //     }

    // }
    // public void InputSFx() {
    //     if (Thruster.activeSelf) {
    //         GameObject.Find("Thruster").GetComponent<ComponentController>().Action(-25);
    //     } else {
    //         GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-25);
    //         GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(-25);
    //     }
        
    // }
    // public void InputAFx() {
    //     if (Thruster.activeSelf) {
    //         GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(25);
    //     } else {
    //         GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(25);
    //         GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(-25);
    //     }
    // }
    // public void InputXFx() {
    //     if (BoosterR.activeSelf)
    //     {
    //         GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(-1);
    //     } else if (CannonR.activeSelf) {
    //         GameObject.Find("CannonL").GetComponent<ComponentController>().Action(-1);
    //     } else {
    //         GameObject.Find("SensorL").GetComponent<ComponentController>().Action(-1);
    //     }
    // }
    // public void InputYFx() {
    //     if (NarrationTimer > 0 && NarrationTimer < 660) { 
    //         NarrationTimer = 660;
    //         if (BoosterR.activeSelf) PlayAudio(TutorialTorpedoFired); 
    //         // if (CannonR.activeSelf) PlayAudio(TutorialCannonFired);
    //         // if (SensorR.activeSelf) PlayAudio(TutorialCannonFired);
            
    //     }
    //     if (BoosterR.activeSelf)
    //     {
    //         GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(-1);
    //         GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-1);
    //     } 
    //    //else if (CannonR.activeSelf) {
    //     //     GameObject.Find("CannonL").GetComponent<ComponentController>().Action(-1);
    //     //     GameObject.Find("CannonR").GetComponent<ComponentController>().Action(-1);
    //     // } else {
    //     //     GameObject.Find("SensorL").GetComponent<ComponentController>().Action(-1);
    //     //     GameObject.Find("SensorR").GetComponent<ComponentController>().Action(-1);
    //     // }
        
    // }
    bool printing = false;
    public void PrinterPrintFx() {
        if (InputField.text.Contains("Printer")) {
            printing = true;
            PrinterLeft.SetActive(false);
            PrinterRight.SetActive(false);
            PrinterPrint.SetActive(false);
            
            OverlayZoomOut.SetActive(true);
            OverlayZoomIn.SetActive(true);
            PlayAudio(WarOfTheWorldsBeepBoop);

            //MapScreenPanOverlay.SetActive(true);
            
            if (NarrationTimer > 0 && NarrationTimer < 200) {
                NarrationTimer = 200;
                // PlayAudio(TutorialTarget);
            }

        } else if (InputField.text.Contains("Process")) {
            // OnCodeView();.
            ResetProcessor();
            
        } else if (InputField.text.Contains("Cannon")) {
            GameObject.Find("CannonL")?.GetComponent<ComponentController>().Action(-1);
            GameObject.Find("CannonR")?.GetComponent<ComponentController>().Action(-1);
            PrinterLeft.SetActive(false);
        } else if (InputField.text.Contains("Left")) {
            GameObject.Find("Left")?.GetComponent<ComponentController>().Action(15);
        } else if (InputField.text.Contains("Right")) {
            GameObject.Find("Right")?.GetComponent<ComponentController>().Action(15);
        } else if (InputField.text.Contains("Scanner")) {
            GameObject.Find("Scanner")?.GetComponent<ComponentController>().Action(0);
        } else if (InputField.text.Contains("Engine")) {
            GameObject.Find("Engine")?.GetComponent<ComponentController>().Action(25);
        } else if (InputField.text.Contains("Turret")) {
            GameObject.Find("Turret")?.GetComponent<ComponentController>().Action(15);
        }

        
        // if (name == "Right" && action == -1) {
        //     GameObject.Find("Right").GetComponent<BoosterController>().Fire();
        // }
        // if (name == "Right" && action != -1) {
        //     GameObject.Find("Right").GetComponent<BoosterController>().Action(action);
        // }
        // if (name == "Left" && action == -1) {
        //     GameObject.Find("Left").GetComponent<BoosterController>().Fire();
        // }
        // if (name == "Left" && action != -1) {
        //     GameObject.Find("Left").GetComponent<BoosterController>().Action(action);
        // }
        // if (name == "Engine") {
        //     GameObject.Find("Engine").GetComponent<ThrusterController>().Action(action);
        // }
        // if (name == "Turret") {
        //     GameObject.Find("Turret").GetComponent<GimbalController>().Action(action);
        // }
        // if (name == "Scanner") {
        //     GameObject.Find("Scanner").GetComponent<SensorController>().Action(action);
        // }
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
    public float GetVolume() {
        return volume_slider.GetComponent<Slider>().value;
    }
    public void SetVolume() 
    {
        if (volume_slider.GetComponent<Slider>().value != camera.GetComponent<AudioSource>().volume) {

            camera.GetComponent<AudioSource>().volume = volume_slider.GetComponent<Slider>().value;
            GameObject.Find("World").GetComponent<AudioSource>().volume = volume_slider.GetComponent<Slider>().value;
            GameObject.Find("Video Player").GetComponent<AudioSource>().volume = volume_slider.GetComponent<Slider>().value / 8;
            Camera.main.GetComponent<CameraController>().bDragging = false;
        }
    }
    public void PlayMusic() {
        PlayMusic(WarOfTheWorldsTheme);
    }
    public void PlayMusic(AudioClip clip) {
        if (GameObject.Find("Video Player") != null) {
            GameObject.Find("Video Player").GetComponent<AudioSource>().clip = clip;
            GameObject.Find("Video Player").GetComponent<AudioSource>().Play();
            GameObject.Find("Video Player").GetComponent<AudioSource>().loop = true;
            GameObject.Find("Video Player").GetComponent<AudioSource>().volume = volume_slider.GetComponent<Slider>().value / 8;
        }
    }
    public void PlayAudio(AudioClip clip) {
        // if (camera != null) {
        GameObject.Find("World").GetComponent<AudioSource>().clip = clip;
        GameObject.Find("World").GetComponent<AudioSource>().loop = false;
        GameObject.Find("World").GetComponent<AudioSource>().Play();
        // }
    }
    string queue_audio = "";
    public void PlayVideo(string url) 
    {
        print (url);
        // LoadingScreen.SetActive(false);
        var trimmed_url = url.Replace(" ", "").Replace("'", "");
        queue_audio = trimmed_url;
        GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().enabled = true;
        #if UNITY_WEBGL
        string asset_location = "https://raw.githubusercontent.com/bitnaughts/bitnaughts.unity/master/Assets/StreamingAssets/BitNaughts" + trimmed_url + "480p.mp4";
        #else 
        string asset_location = System.IO.Path.Combine (Application.streamingAssetsPath, "BitNaughts" + trimmed_url + "480p.mp4");
        #endif
        // print (asset_location);
        GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().url = asset_location; 
        GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().Play();
        SetBackground(new Color(20f/255f, 20f/255f, 20f/255f));
        MapScreenPanOverlay.SetActive(false);
        OverlayZoomIn.SetActive(false);
        OverlayZoomOut.SetActive(false);
        volume_slider.SetActive(true);
        InterpreterZoomIn.SetActive(true);
        InterpreterZoomOut.SetActive(true);
        InputJoystick.SetActive(false);
        InputUseWeapon.SetActive(false);
        // SubtitlesShadow.SetActive(true);
        // Subtitles.SetActive(true);
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
        print (clip);
        switch (clip)
        {
            case "Loading":
                return LoadingNarration;
            case "Hook":
                return HookNarration;
            case "SplashScreen":
                return SplashScreenNarration;
            case "WarOfTheWorldsCredit":
                return WarOfTheWorldsCredit;
            case "WarOfTheWorldsIntro":
                return WarOfTheWorldsIntro;
            case "WarOfTheWorldsStinger":
                return WarOfTheWorldsStinger;
            case "WarOfTheWorldsFirstContact":
                return WarOfTheWorldsFirstContact;
            case "WarOfTheWorldsHeatRay":
                return WarOfTheWorldsHeatRay;
            case "WarOfTheWorldsRedCross":
                return WarOfTheWorldsRedCross;
            case "WarOfTheWorldsGroversMill":
                return WarOfTheWorldsGroversMill;
            case "TutorialIntroduction":
                return TutorialIntroduction;
            case "Cosmos0":
                return CampaignCosmos0;
            case "Cosmos1":
                return CampaignCosmos1;
            case "Cosmos2":
                return CampaignCosmos2;
            case "Cosmos3":
                return CampaignCosmos3;
            case "Cosmos4":
                return CampaignCosmos4;
            case "Cosmos5":
                return CampaignCosmos5;
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
        InterpreterPanel.SetActive(true);
        // TabToggle.text = "▦ GUI";
        
        // TabToggle.text = "▤ TUI";
        // OverlayZoomIn.SetActive(true);
        // OverlayZoomOut.SetActive(true);
        // OverlayPanUp
        // OverlayPanDown
        
        // volume_slider.SetActive(false);
    }
    public void OnCodeView() {
        MapPanel.SetActive(true);
        InterpreterPanel.SetActive(true);
        // TabToggle.text = "▤ TUI";
        // volume_slider.SetActive(true);
        // 
        
        InterpreterZoomIn.SetActive(true);
        InterpreterZoomOut.SetActive(true);
    }
    public void AppendText(string text) {
        if (history.LastIndexOf("$") != -1) history = history.Substring(0, history.LastIndexOf("$"));
        history += text;
        RenderText(history);
    }
    public void ClearText() {
        if (history == "") history = "$";
        InputField.text = "☄ BitNaughts";
        component_name = "";
        // MapScreenPanOverlay.SetActive(true);
        // volume_slider.SetActive(false);
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
            if (lines[line].Contains("<color=#")) {
                if (character_count > max_line_length + 18) {
                    max_line_length = character_count - 18;
                }
            } else {
                if (character_count > max_line_length) {
                    max_line_length = character_count;
                }
            }
        }
        SetContentSize((max_line_length - 2) * fontSize / 2, lines.Length * fontSize);
    }

    public string component_name = "";
    public string component_text = "";
    public void RenderComponent(string component) {
        if (component == "") return;
        var component_string = "";
        if (component[1] == ' ') component = component.Substring(2);
        if (Ship.IsComponent(component)) {
            component_string = Ship.GetComponentToString(component);
        }
        if (Enemy.IsComponent(component)) {
            component_string = Enemy.GetComponentToString(component);
        }

        component_name = component_string.Substring(0, component_string.IndexOf("\n"));// + component_string.Substring(component_string.IndexOf("class ") + 6, component_string.IndexOf(":") - (component_string.IndexOf("class ") + 6));
        // print("\"" + component_name + "\"");
        InputField.text = component_name;
        InterpreterZoomIn.SetActive(true);
        InterpreterZoomOut.SetActive(true);
        volume_slider.SetActive(false);
        if (InputField.text.Contains("Printer")) {
            // InputField.text = "▦ Printer";//" ⛴ Ship Select";
            switch (MarkerIndex) {
                case 0:

                    Processor.SetActive(true);
                    CampaignIntroAssets.SetActive(true);
                    CampaignIntroSatelliteAssets.SetActive(true);
                    Ship.Start();
                    Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){
                        new ClassObj("▩ ScanProcess"),
                        new ClassObj("▣ Turret"),
                        new ClassObj("◌ Scanner"),//,
                        new ClassObj("◎ Right"),
                        new ClassObj("◎ Left"),
                        new ClassObj("◉ Engine")
                        // new ClassObj("▨ Antenna")
                    });
                    //         var components = new string[]{"Processor Process = new Processor (0, 3, 4, 5);","Bulkhead Bulk = new Bulkhead (0, 0, 4, 4);","Booster Right = new Booster (4, 3, 2, 1);", "Booster Left = new Booster (1, 2, 3, 4);", "Thruster Engine = new Thruster (3, 4, 5, 6);"};

                    // Bulkhead.SetActive(true);
                    // Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){
                    //     new ClassObj("▩ Process"),
                    //     new ClassObj("▥ Bulk"),
                    //     new ClassObj("◉ Engine"),
                    //     new ClassObj("◎ Right"),
                    //     new ClassObj("◎ Left")
                    // });
                    // Thruster.SetActive(true);
                    // BoosterL.SetActive(true);
                    // BoosterR.SetActive(true);
                    if (NarrationTimer < 120) NarrationTimer = 120;
                    // Ship.Start();
                    if (GameObject.Find("0") != null) {
                        GameObject.Find("0").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                        GameObject.Find("0").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Exiting";
                        GameObject.Find("0").SetActive(false);

                    }
                    break;
                case 1:
                    
                    Processor.SetActive(true);
                    if (NarrationTimer < 3000) {

                        CampaignIntroAssets.SetActive(true);
                        CampaignIntroSatelliteAssets.SetActive(true);
                        
                        Ship.Start();
                        Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){
                            new ClassObj("▩ ScanProcess"),
                            new ClassObj("▣ Turret"),
                            new ClassObj("◉ Engine"),
                            new ClassObj("◌ Scanner")//,
                            // new ClassObj("▨ Antenna")
                        });
                    }
                    else if (NarrationTimer < 4000) {
                        CampaignGroverAssets.SetActive(true);
                        CampaignGroverSatelliteAssets.SetActive(true);
                        
                        Ship.Start();
                        Processor.GetComponent<ProcessorController>().interpreter = new InterpreterV3(new List<ClassObj>(){
                            new ClassObj("▩ GroverProcess"),
                            new ClassObj("▣ Swivel"),
                            new ClassObj("◌ Scanner"),
                            new ClassObj("◉ LeftEngine"),
                            new ClassObj("◉ RightEngine"),
                            new ClassObj("◍ LeftCannon"),
                            new ClassObj("◍ RightCannon"),
                        });
                    }
                    if (GameObject.Find("1") != null) {
                        GameObject.Find("1").SetActive(false);
                    }
                    break;
                case 2:
                    if (GameObject.Find("2") != null) {
                        GameObject.Find("2").SetActive(false);
                    }
                    break;
                case 3:
                    if (GameObject.Find("3") != null) {
                        GameObject.Find("3").SetActive(false);
                    }
                    break;
                case 4:
                    if (GameObject.Find("4") != null) {
                        GameObject.Find("4").SetActive(false);
                    }
                    break;
                case 5:
                    if (GameObject.Find("5") != null) {
                        GameObject.Find("5").SetActive(false);
                    }
                    break;

            }
            // PrinterLeft.SetActive(true);
            // PrinterRight.SetActive(true);
            PrinterPrint.SetActive(true);
            PrinterPrint.transform.GetChild(0).GetComponent<Text>().text = "☈ Print";
            // if (NarrationTimer > 0 && NarrationTimer < 240) {
            //     NarrationTimer = 240;
            //     PlayAudio(TutorialComponentsIcons);
            // }
        }
        else if (InputField.text.Contains("Process")) {
            // InputField.text = "▩ " + InputField.text;
            PrinterPrint.SetActive(true);
            PrinterPrint.transform.GetChild(0).GetComponent<Text>().text = "☇ Main";
            // PrinterRight.SetActive(false);
            // PrinterLeft.SetActive(false);
        }
        else if (InputField.text.Contains("◍")) {
            // InputField.text = "◍ " + InputField.text;
            PrinterPrint.SetActive(true);
            PrinterPrint.transform.GetChild(0).GetComponent<Text>().text = "◍ Fire";
            // PrinterRight.SetActive(false);
            // PrinterLeft.SetActive(false);
        }
        else if (InputField.text.Contains("◎")) {
            // InputField.text = "◎ " + InputField.text;
            PrinterPrint.SetActive(true);
            PrinterPrint.transform.GetChild(0).GetComponent<Text>().text = "◎ Boost";
            // PrinterRight.SetActive(true);
            // PrinterLeft.SetActive(true);
        }
        else if (InputField.text.Contains("◌")) {
            // InputField.text = "◌ " + InputField.text;
            PrinterPrint.SetActive(true);
            PrinterPrint.transform.GetChild(0).GetComponent<Text>().text = "◌ Scan";
            // PrinterRight.SetActive(true);
            // PrinterLeft.SetActive(true);
        }
        else if (InputField.text.Contains("◉")) {
            // InputField.text = "◉ " + InputField.text;
            PrinterPrint.SetActive(true);
            PrinterPrint.transform.GetChild(0).GetComponent<Text>().text = "◉ Throttle";
            // PrinterRight.SetActive(false);
            // PrinterLeft.SetActive(false);
        }
        else if (InputField.text.Contains("▣")) {
            // InputField.text = "◉ " + InputField.text;
            PrinterPrint.SetActive(true);
            PrinterPrint.transform.GetChild(0).GetComponent<Text>().text = "▣ Rotate";
            // PrinterRight.SetActive(false);
            // PrinterLeft.SetActive(false);
        }
        if (GameObject.Find("OverlayDropdownLabel") != null) GameObject.Find("OverlayDropdownLabel").GetComponent<Text>().text = component_name;
        component_text = component_string.Substring(component_string.IndexOf("\n") + 1);
        RenderText(component_text);
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
        if (InputField.text[1] == ' ') return InputField.text.Substring(2);
        return InputField.text;
    }
    public int fontSize = 75;
    public void OnZoomIn() {
        fontSize += 5;
        if (fontSize > 100) fontSize = 100;
        if (component_name == "") ClearText();//RenderText(history);
        // if RenderComponent("Process");
    }
    public void OnZoomOut() {
        fontSize -= 5;
        if (fontSize < 10) fontSize = 10;
        if (component_name == "") ClearText();//RenderText(history);
        // RenderComponent("Process");
    }
    
    public void OnInputFire() {
        Processor.GetComponent<ProcessorController>().interpreter_input.fire = true;
    }
    public void OnInput() {
        switch (GetCommand()) {
            case "nano":
                var component_gameObject = Instantiate(Overlay, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                component_gameObject.name = InputField.text;//object_reference.name + component_count;
                component_gameObject.GetComponent<SpriteRenderer>().size = new Vector2(2,2);//object_reference.GetComponent<ComponentController>().GetMinimumSize();
                component_gameObject.transform.SetParent(Ship.transform.Find("Rotator"));
                Ship.Start();
                OverlayInteractor.UpdateOptions();
            break;
            default:
                GameObject.Find(component_name).name = InputField.text;
                Ship.Start();
                OverlayInteractor.UpdateOptions();
                OverlayInteractor.OnDropdownChange(0); 
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
    public void UseWeapon() {
        Action("Cannon", -1);//GetInput(), -1);
        if (clip_index == 2 && campaign_stage == 2) { campaign_stage++; story_timer = 0f; }
    }
    public void CycleTutorial() {
        if (NarrationTimer > 0 && NarrationTimer < 420) {
            PlayAudio(TutorialGood2);
            NarrationTimer = 420;
        }
        else if (NarrationTimer > 0 && NarrationTimer < 480) {
            PlayAudio(TutorialGood2);
            NarrationTimer = 480;
        }
        else if (NarrationTimer > 0 && NarrationTimer < 540) {
            PlayAudio(TutorialGood3);
            NarrationTimer = 540;
        }
        else if (NarrationTimer > 0 && NarrationTimer < 600) {
            // PlayAudio(TutorialGetMoving);
            // InputJoystick.SetActive(true);
            NarrationTimer = 600;
        }
        else if (NarrationTimer > 2000 && NarrationTimer < 2200) {
            NarrationTimer = 2198;
            // PlayVideo("Cosmos2");
        }
    }
    // public void BinocularTutorial() {
    //     if (NarrationTimer > 0 && NarrationTimer < 840) {
    //         NarrationTimer = 840;
    //         PlayAudio(TutorialGood2);
    //         BinocularToggle.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    //     }
    // }
    public void PanTutorial() {
        // if (NarrationTimer > 300 && NarrationTimer < 360) { 
        //     NarrationTimer = 360; 
        //     PlayAudio(TutorialCycle); 
        //     CycleToggle.SetActive(true);
        //     if (GameObject.Find("OverlayPanUp") != null) GameObject.Find("OverlayPanUp").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        //     if (GameObject.Find("OverlayPanDown") != null) GameObject.Find("OverlayPanDown").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        //     if (GameObject.Find("OverlayPanRight") != null) GameObject.Find("OverlayPanRight").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        //     if (GameObject.Find("OverlayPanLeft") != null) GameObject.Find("OverlayPanLeft").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            
        // }
    }
    public void CancelTutorial() {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
    public void ThrustTutorial() {
    }
    public void Unzoom() {
        TutorialAssets.SetActive(false);
        // SubtitlesShadow.SetActive(false);
        // Subtitles.SetActive(false);
        CycleToggle.SetActive(false);
        BinocularToggle.SetActive(false);
        InputUseWeapon.SetActive(false);
        InputJoystick.SetActive(false);
        Stage = "MapUnzoom";
        camera.GetComponent<AudioSource>().Stop();
        // NarrationTimer = -1f;
        for (int i = 0; i < Example.transform.GetChild(0).childCount; i++) {
            if (Example.transform.GetChild(0).GetChild(i).gameObject.name != "Main Camera") 
                Example.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
        }
    }
    public void FinishTutorial() {
        // Sound("Back");
        if (Stage == "MapZoomed") {
            NarrationIndex = 0;
            NarrationTimer = -1;
            
            PlayAudio(SoundBack);
            Subtitles.SetActive(false);
            SubtitlesShadow.SetActive(false);
            CycleToggle.SetActive(false);
            BinocularToggle.SetActive(false);
            InputUseWeapon.SetActive(false);
            InputJoystick.SetActive(false);
            Unzoom();
        } 
    }
    public void OnExit() {
        // Application.Quit();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
    public void Action(string name, int action) {
        print ("ACTION" + name + " " + action);
        if (name == "Right" && action == -1) {
            GameObject.Find("Right").GetComponent<BoosterController>().Fire();
        }
        if (name == "Right" && action != -1) {
            GameObject.Find("Right").GetComponent<BoosterController>().Action(action);
        }
        if (name == "Left" && action == -1) {
            GameObject.Find("Left").GetComponent<BoosterController>().Fire();
        }
        if (name == "Left" && action != -1) {
            GameObject.Find("Left").GetComponent<BoosterController>().Action(action);
        }
        if (name == "Engine") {
            GameObject.Find("Engine").GetComponent<ThrusterController>().Action(action);
        }
        if (name == "Turret") {
            GameObject.Find("Turret").GetComponent<GimbalController>().Action(action);
        }
        if (name == "Scanner") {
            GameObject.Find("Scanner").GetComponent<SensorController>().Action(action);
        }
        // GameObject.Find(name).GetComponent<ComponentController>().Action(action);
    }
    public void Sound(string clip) {
        switch (clip) {
            case "Back": PlayAudio(SoundBack); break;
            case "Click": PlayAudio(SoundClick); break;
            case "Error": PlayAudio(SoundError); break;
            case "OnMouse": PlayAudio(SoundOnMouse); break;
            case "Toggle": PlayAudio(SoundToggle); break;
            case "Processor": PlayAudio(SoundProcessor); break;
            case "Gimbal": PlayAudio(SoundGimbal); break;
            case "Cannon1": PlayAudio(SoundCannon1); break;
            case "Cannon2": PlayAudio(SoundCannon2); break;
            case "Cannon3": PlayAudio(SoundCannon3); break;
            case "Radar": PlayAudio(SoundRadar); break;
            case "Booster": PlayAudio(SoundBooster); break;
            case "Thruster": PlayAudio(SoundThruster); break;
            case "Torpedo1": PlayAudio(SoundTorpedo1); break;
            case "Torpedo2": PlayAudio(SoundTorpedo2); break;
            case "Warning": PlayAudio(SoundWarning); break;
            case "WarningOver": PlayAudio(SoundWarningOver); break;
            case "TryAgain": PlayAudio(WarOfTheWorldsTryAgain); break;
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
    }
    void MapSubtitlesAtTime(string text, float time, float timer) {
        if (timer >= time && timer < time + (Time.deltaTime * 2f)) {
            // if (text.Contains("<b>")) Subtitles.GetComponent<Text>().text = text.Substring(text.IndexOf("<b>") + "<b>".Length, text.IndexOf("</b>") - text.IndexOf("<b>") - "<b>".Length);
            // else Subtitles.GetComponent<Text>().text = text;
            RenderText(text);
        }
    }
    void SubtitlesAtTime(string text, float time) {
        if (timer >= time && timer < time + (Time.deltaTime * 2f)) {
            // if (text.Contains("<b>")) Subtitles.GetComponent<Text>().text = text.Substring(text.IndexOf("<b>") + "<b>".Length, text.IndexOf("</b>") - text.IndexOf("<b>") - "<b>".Length);
            // else Subtitles.GetComponent<Text>().text = text;
            RenderText(text);
        }
    }
    void MapSubtitlesAtTime(string text, float time) {
        if (timer >= time && timer < time + (Time.deltaTime * 2f)) {
            // if (text.Contains("<b>")) Subtitles.GetComponent<Text>().text = text.Substring(text.IndexOf("<b>") + "<b>".Length, text.IndexOf("</b>") - text.IndexOf("<b>") - "<b>".Length);
            // else Subtitles.GetComponent<Text>().text = text;
            RenderText(text);
        }
    }
    void MapSubtitles(string text) {
            // if (text.Contains("<b>")) Subtitles.GetComponent<Text>().text = text.Substring(text.IndexOf("<b>") + "<b>".Length, text.IndexOf("</b>") - text.IndexOf("<b>") - "<b>".Length);
            // else Subtitles.GetComponent<Text>().text = text;
        RenderText(text);
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
    GameObject MapMarker;
    void Update () {
        animation_timer += Time.deltaTime;
        if (Input.GetMouseButton(0)) {
            click_duration += Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0)) {
            click_duration = 0;
        }
        if (Stage == "SplashScreen" && NarrationTimer > -1f) {
                OverlayZoomIn.SetActive(true);
                OverlayZoomOut.SetActive(true);
                MapScreenPanOverlay.SetActive(true);
                print (NarrationTimer);
                NarrationTimer = 0;
                // SubtitlesShadow.SetActive(false);
                // Subtitles.SetActive(false);
                camera.GetComponent<AudioSource>().Stop();
                Stage = "MapInterface";
                SplashScreen.SetActive(false);
                InputField.text = "☄ BitNaughts";
                component_name = "";
                ResetVideo();
                MapUnzoomed();
                PlayMusic(WarOfTheWorldsHum);
                PlayAudio(WarOfTheWorldsBeep);
                // Map.SetEarth();
                volume_slider.SetActive(false);
                InterpreterZoomIn.SetActive(true);
                InterpreterZoomOut.SetActive(true);
                MapMarker = GameObject.Find("0");

        } 
        if (NarrationTimer > 1 && NarrationTimer < 2) {
            PlayAudio(WarOfTheWorldsMapScreen);
            NarrationTimer = 2;
        }
        if (NarrationTimer >= 2 && NarrationTimer < 5) {
            GameObject.Find("MapScreenOverlay").GetComponent<Image>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
        }
        if (NarrationTimer >= 6 && NarrationTimer < 7) {
            GameObject.Find("MapScreenOverlay").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        if (NarrationTimer >= 6 && NarrationTimer < 61) {
            MapMarker.GetComponent<SpriteRenderer>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
        }
        if (NarrationTimer >= 61 && NarrationTimer < 62) {
            GameObject.Find("MapScreenOverlay").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            MapMarker.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            PlayAudio(NarratorZoomInDetails);
            NarrationTimer = 62;
        }
        if (NarrationTimer >= 62 && NarrationTimer < 99) {
            GameObject.Find("OverlayZoomIn").GetComponent<Image>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
        }
        if (NarrationTimer > 99 && NarrationTimer < 100) {
            GameObject.Find("MapScreenOverlay").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            GameObject.Find("OverlayZoomIn").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            NarrationTimer = 100;
            PlayAudio(WarOfTheWorldsTargetWindow);
        }
        if (NarrationTimer >= 100 && NarrationTimer < 120) {
            Printer.GetComponent<SpriteRenderer>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
        }
        if (NarrationTimer > 120 && NarrationTimer < 121) {
            Printer.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            NarrationTimer = 121;
            PlayAudio(WarOfTheWorldsTargetWindowGood);
        }
        if (NarrationTimer > 121 && NarrationTimer < 128) {
            if (GameObject.Find("OverlayBorder") != null) GameObject.Find("OverlayBorder").GetComponent<Image>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
        }
        if (NarrationTimer > 128 && NarrationTimer < 129) {
            if (GameObject.Find("OverlayBorder") != null) GameObject.Find("OverlayBorder").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            NarrationTimer = 129;
            PlayAudio(WarOfTheWorldsTargetWindowIssueOrder);
        }
        if (NarrationTimer > 129 && NarrationTimer < 180) {
            PrinterPrint.GetComponent<Image>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
        }
        if (NarrationTimer > 180 && NarrationTimer < 181) {
            
            PrinterPrint.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            NarrationTimer = 181;
            // PlayAudio(WarOfTheWorldsTargetWindowIssueOrder);
        }
        if (NarrationTimer > 204 && NarrationTimer < 205) {
            PlayAudio(WarOfTheWorldsGetMoving);
            // PlayMusic(WarOfTheWorldsTheme);
            if (GameObject.Find("OverlayBorder") != null) GameObject.Find("OverlayBorder").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            NarrationTimer = 205;
        }
        if (NarrationTimer > 205f && NarrationTimer < 209f) {
            InputJoystick.transform.GetChild(0).GetComponent<Image>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
        }
        if (NarrationTimer > 209f && NarrationTimer < 210) {
            // PlayAudio(WarOfTheWorldsFirstContact);
            NarrationTimer = 210;
            InputJoystick.transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        if (NarrationTimer > 300 && NarrationTimer < 330) {
            // GameObject.Find("OverlayZoomOut").GetComponent<Image>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);

        }
        if (NarrationTimer > 330 && NarrationTimer < 331) {
            // GameObject.Find("OverlayZoomOut").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            // NarrationTimer = 331;
            // Unzoom();
        }
        if (Input.GetMouseButtonUp(0) && CheckInsideEdge()) //&& GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().frame != -1) || ((ulong)GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().frame >= GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().frameCount - 1 && GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().frame > 0))
        {
            ResetVideo();
            LoadingScreen.SetActive(false);
            if (Stage == "SplashScreen") // Splash Screen
            {
                // Printer.SetActive(false);
                OverlayZoomIn.SetActive(true);
                OverlayZoomOut.SetActive(true);
                MapScreenPanOverlay.SetActive(true);
                print (NarrationTimer);
                NarrationTimer = 0;
                // SubtitlesShadow.SetActive(false);
                // Subtitles.SetActive(false);
                camera.GetComponent<AudioSource>().Stop();
                Stage = "MapInterface";
                SplashScreen.SetActive(false);
                InputField.text = "☄ BitNaughts";
                component_name = "";
                ResetVideo();
                MapUnzoomed();
                // Map.SetEarth();
                volume_slider.SetActive(false);
                InterpreterZoomIn.SetActive(true);
                InterpreterZoomOut.SetActive(true);
                PlayMusic(WarOfTheWorldsHum);
                PlayAudio(WarOfTheWorldsBeep);
                MapMarker = GameObject.Find("0");
                // PlayAudio(SplashScreenComplete);
            } else {
                // if (NarrationTimer >= 100 && NarrationTimer < 180) {
                //     Stage = "MapZoomed"; 
                //     NarrationTimer = 180;
                //     // PlayAudio(NarratorWelcome);
                    
                //     PlayAudio(TutorialLookAround);
                //     PlayMusic(IntroMusic);
                //     ResetVideo();
                //     InputField.text = "☄ Tutorial";
                    
                //     Printer.SetActive(true);
                //     Ship.Start();
                //     // Map.SetEarth();
                // } else if (NarrationTimer >= 1100 && NarrationTimer < 1200) {
                //     Stage = "MapZoomed";
                //     NarrationTimer = 1200;
                //     NarrationIndex = 0;
                //     GameObject.Find("World").GetComponent<AudioSource>().Stop();
                //     PlayMusic(CampaignPearlMusic);
                //     ResetVideo();
                //     Map.SetMars();
                    
                //     Printer.SetActive(true);
                //     Ship.Start();
                //     MapScreenPanOverlay.SetActive(true);
                //     volume_slider.SetActive(false);
                //     OverlayZoomIn.SetActive(true);
                //     OverlayZoomOut.SetActive(true);
                //     camera.GetComponent<Camera>().backgroundColor = new Color(80f/255f, 80f/255f, 80f/255f);
                //     // PlayAudio(CampaignPearlIntroduction);
                //     InputField.text = "☄ Mars";
                // } else if (NarrationTimer >= 2000 && NarrationTimer <= 3000) {
                //     Map.SetEarth();
                //     Map.SetGroversMill();
                //     InputField.text = "☄ BitNaughts";
                //     MapScreenPanOverlay.SetActive(true);
                //     volume_slider.SetActive(false);
                //     OverlayZoomIn.SetActive(true);
                //     OverlayZoomOut.SetActive(true);
                //     // PlayAudio(NarratorWelcome);
                //     ResetVideo();
                //     NarrationTimer = 3100;
                //     if (GameObject.Find("1") != null) GameObject.Find("1").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Campaign";
                // }
                // } else if (NarrationTimer >= 1999 && NarrationTimer < 2100){
                //     Stage = "MapZoomed";
                //     // NarrationTimer = 2100;
                //     // PlayMusic(CampaignMidwayMusic);
                //     ResetVideo();

                //     MapScreenPanOverlay.SetActive(true);
                //     OverlayZoomIn.SetActive(true);
                //     OverlayZoomOut.SetActive(true);
                //     BinocularToggle.SetActive(true);
                //     CycleToggle.SetActive(true);
                //     // volume_slider.SetActive(false);
                //     InputField.text = "☄ BitNaughts";
                //     component_name = "";
                // } else if (NarrationTimer >= 2100 && NarrationTimer < 2166){
                //     Stage = "MapZoomed";
                //     // NarrationTimer = 2166;
                //     // PlayMusic(CampaignMidwayMusic);
                //     ResetVideo();
                    
                //     MapScreenPanOverlay.SetActive(true);
                //     OverlayZoomIn.SetActive(true);
                //     OverlayZoomOut.SetActive(true);
                //     BinocularToggle.SetActive(true);
                //     CycleToggle.SetActive(true);
                //     // volume_slider.SetActive(false);
                //     InputField.text = "☄ BitNaughts";
                //     component_name = "";
                // } else if (NarrationTimer >= 2200 && NarrationTimer < 2278){
                //     Stage = "MapZoomed";
                //     // NarrationTimer = 2278;
                //     // PlayMusic(CampaignMidwayMusic);
                    
                //     MapScreenPanOverlay.SetActive(true);
                //     OverlayZoomIn.SetActive(true);
                //     OverlayZoomOut.SetActive(true);
                //     BinocularToggle.SetActive(true);
                //     CycleToggle.SetActive(true);
                //     // volume_slider.SetActive(false);
                //     InputField.text = "☄ BitNaughts";
                //     component_name = "";
                // }
                // else if (NarrationTimer > 2300 && NarrationTimer < 2400 && CampaignIntroAssets.transform.GetChild(0).childCount == 0) {
                //     NarrationTimer = 2400;
                //     ResetVideo();
                //     Unzoom();
                // }
            }
        }
        // if (Input.GetKeyDown("x")) {
        //     InputYFx();
        // }
        // if (Input.GetKey("w")) {
        //     if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(0.5f);
        //     if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(0.5f);
        //     if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(0.5f);
        //     if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(0.5f);
        //     if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(0.5f);
        //     if (NarrationTimer > 0 && NarrationTimer < 842) { NarrationTimer = 842; InputJoystick.transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f); }
        // }
        // if (Input.GetKey("q")) {
        //     if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(0.5f);
        //     if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(-0.5f);
        //     if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(0.5f);
        //     if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(-0.5f);
        //     if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(0.5f);
        //     if (NarrationTimer > 0 && NarrationTimer < 842) { NarrationTimer = 842; InputJoystick.transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f); }
        // }
        // if (Input.GetKey("e")) {
        //     if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(0.5f);
        //     if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(0.5f);
        //     if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-0.5f);
        //     if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(0.5f);
        //     if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-0.5f);
        //     if (NarrationTimer > 0 && NarrationTimer < 842) { NarrationTimer = 842; InputJoystick.transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f); }
        // }
        // if (Input.GetKey("a")) {
        //     if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(-0.5f);
        //     if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(0.5f);
        //     if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(-0.5f);
        //     if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(0.5f);
        // }
        // if (Input.GetKey("d")) {
        //     if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(0.5f);
        //     if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-0.5f);
        //     if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(0.5f);
        //     if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-0.5f);
        // }
        // if (Input.GetKey("s")) {
        //     if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(-0.5f);
        //     if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(-0.5f);
        //     if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-0.5f);
        //     if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(-0.5f);
        //     if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-0.5f);
        // }
        // if (Input.GetKey("z")) {
        //     if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(-0.5f);
        //     if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(-0.5f);
        //     if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(0.5f);
        //     if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(-0.5f);
        //     if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(0.5f);
        // }
        // if (Input.GetKey("c")) {
        //     if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(-0.5f);
        //     if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(0.5f);
        //     if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-0.5f);
        //     if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(0.5f);
        //     if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-0.5f);
        // }
        // Gamepad gamepad = Gamepad.current;
        // if (gamepad != null)
        // {
        //     Vector2 stickL = gamepad.leftStick.ReadValue(); 
        //     if (stickL != Vector2.zero) Camera.main.GetComponent<CameraController>().bDragging = false;
        //     if (stickL.y < -1/5f) {
        //         // if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(stickL.y * 5);
        //         // if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(stickL.y * 5);
        //         // if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(stickL.y * 5);
        //         // if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(stickL.y * 5);
        //         // if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(stickL.y * 5);
        //     }
        //     if (stickL.y > 1/5f) {
        //         // if (GameObject.Find("Thruster")) GameObject.Find("Thruster").GetComponent<ComponentController>().Action(stickL.y * 5);
        //         // if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(stickL.y * 5);
        //         // if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(stickL.y * 5);
        //         // if (NarrationTimer > 0 && NarrationTimer < 842) { NarrationTimer = 842; InputJoystick.transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f); }
        //     }
        //     if (stickL.x < -1/5f) {
        //         // if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(stickL.x * 5);
        //         // if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-stickL.x * 5);
        //         // if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(stickL.x * 5);
        //         // if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-stickL.x * 5);
        //     }
        //     if (stickL.x > 1/5f) {
        //         // if (GameObject.Find("ThrusterL")) GameObject.Find("ThrusterL").GetComponent<ComponentController>().Action(stickL.x * 5);
        //         // if (GameObject.Find("ThrusterR")) GameObject.Find("ThrusterR").GetComponent<ComponentController>().Action(-stickL.x * 5);
        //         // if (GameObject.Find("BoosterL")) GameObject.Find("BoosterL").GetComponent<ComponentController>().Action(stickL.x * 5);
        //         // if (GameObject.Find("BoosterR")) GameObject.Find("BoosterR").GetComponent<ComponentController>().Action(-stickL.x * 5);
        //     }
        // }
    }
    bool CheckInsideEdge() {
        // TODO
        // Needs to check horizonal versus verticle,
        // if over volume slider...
        return (Input.mousePosition.y > 100 && Input.mousePosition.y < Screen.height - 160 && Input.mousePosition.x > 100 && Input.mousePosition.x < Screen.width - 100); //&& Input.mousePosition.x - Screen);
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
        Camera.main.orthographicSize = 6;
        Stage = "MapZoomed";
        Ship.Start();
        OverlayInteractor.UpdateOptions();
        // MapScreenPanOverlay.SetActive(true);
        // GameObject.Find("OverlayPanDown")?.SetActive(false);
        if (BinocularToggle != null) BinocularToggle.SetActive(false);
        if (CycleToggle != null) CycleToggle.SetActive(false);
        GameObject.Find("OverlayZoomIn").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        switch (MarkerIndex){
            case 0:
                Printer.SetActive(true);      
                Ship.Start();
                OverlayInteractor.UpdateOptions();
                // PlayVideo("NewtonsLaws");
                // PlayAudio(LookupNarration("NewtonsLaws"));
                NarrationTimer = 99;
                TutorialAssets.SetActive(true);
                Printer.GetComponent<PrinterController>().components_declarations = new string[] {"var Process = new Processor (0, 2.5, 4, 5);", "var Scanner = new Sensor (0, -1, 3, 3);", "var Turret = new Gimbal (0, -1, 3, 3);",  "var Left = new Booster (-2.5, -1, 2, 3);", "var Right = new Booster (2.5, -1, 2, 3);", "var Engine = new Thruster (0, -3.5, 6, 3);"};
                
                GameObject.Find("0").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Printer";
                // GameObject.Find("0").SetActive(false);
                break;
            case 1:
                Printer.SetActive(true);           
                Ship.Start();
                NarrationIndex = 0;
                if (NarrationTimer < 1100) {
                    
                    PlayVideo("WarOfTheWorldsFirstContact");
                    NarrationTimer = 1100;
                }
                else if (NarrationTimer < 3300) {
                    PlayVideo("WarOfTheWorldsGroversMill");
                    NarrationTimer = 3300;

                }
                // GameObject.Find("1").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "MK-31";
                // PlayVideo("WarOfTheWorldsIntro");
                // PlayVideo("MoravecsParadox");
                // PlayAudio(CampaignPearlIntroduction);CampaignIntroAssets
                // CampaignIntroAssets.SetActive(true);
                break;
            case 2:
                PlayVideo("NewtonsLaws");
                // PlayAudio(CampaignMidwayIntroduction);
                NarrationTimer = 2600;
                CampaignPearlAssets.SetActive(true);
                break;
            case 3:
                PlayVideo("HawkingRadiation");
                CampaignMidwayAssets.SetActive(true);
                // PlayAudio(CampaignMidwayIntroduction);
                NarrationTimer = 2999;
                // CampaignMidwayAssets.SetActive(true);
                break;
            case 4:
                PlayVideo("DopplerEffect");
                // PlayAudio(CampaignMidwayIntroduction);
                NarrationTimer = 3999;
                // CampaignMidwayAssets.SetActive(true);
                break;
            case 5:
                PlayVideo("DopplerShift");
                // PlayAudio(CampaignMidwayIntroduction);
                NarrationTimer = 4999;
                // CampaignMidwayAssets.SetActive(true);
                break;
        }

        Map?.Zoom(15);//15);
    }
    public void MapUnzoomed() {

        Stage = "MapInterface";
        Map?.Zoom(0.825f);
        if (NarrationTimer > 2000) {
            NarrationTimer = 2100;
            GameObject.Find("Video Player").GetComponent<AudioSource>().Stop();
            // PlayVideo("WarOfTheWorldsHeatRay");
            CampaignIntroAssets.SetActive(false);
            CampaignIntroSatelliteAssets.SetActive(false);
        }     
        if (NarrationTimer > 300 && NarrationTimer < 340) {           
            GameObject.Find("0").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Navigate";
            NarrationTimer = 340;
        }
    }
    public void MapUnzoom() {
        if (Stage == "MapZoom") {
            print ("unzoom");
            PlayAudio(SoundBack);
            NarrationIndex = -1;  
            // SubtitlesShadow.SetActive(false);
            // Subtitles.SetActive(false);   S
            Unzoom();
        } else {
            if (NarrationTimer > 300 && NarrationTimer < 330) {
                Unzoom();
                CampaignIntroAssets.SetActive(false);
                CampaignIntroSatelliteAssets.SetActive(false);
                NarrationTimer = 330;
            }
        }
    }
    public void MapZoom() {
        OverlayZoomOut.SetActive(true);
        if (Stage == "MapZoom") {
            MapZoomed();
        } else {
            // Stage = "MapZoom";
            switch (MarkerIndex) {
                case 0:
                    // GameObject.Find("0").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    // GameObject.Find("0").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Zooming";
                    // PlayAudio(NarratorZoomInDetails);
                    break;
                case 1:
                    break;
                case 2:
                    // NarrationTimer = 2999;
                    break;
                default:
                    // PlayMusic(MultiplayerSelectMusic);
                    
                    // Stage = "MapInterface";
                    break;
            }
        }
        
    }
    public int MarkerIndex = -1;
    public void MapInteractor(string marker) {
        print (marker);
        // Sound("OnMouse");
        PlayAudio(WarOfTheWorldsClick);
        var target = GameObject.Find(marker);
        Camera.main.transform.localPosition = new Vector3(target.transform.position.x, target.transform.position.z, -100);
        if (int.TryParse(marker, out MarkerIndex)) {
            if (GameObject.Find("0") != null) GameObject.Find("0").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);               
            // print (MarkerIndex);
            if (MarkerIndex == 0) {
                // if (TutorialAssets.transform.childCount == 0) {
                //     PlayAudio(TutorialOutro);
                // }
                // else {
                GameObject.Find("0").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Starting";
                MapScreenPanOverlay.SetActive(false);
                volume_slider.SetActive(true);
                global_timer = 0;
                NarrationTimer = 61f;
                NarrationIndex = 0;
                Stage = "MapZoom";
                // } 
                // GameObject.Find("0").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Zooming";
                // SubtitlesShadow.SetActive(true);
                // Subtitles.SetActive(true);
            } else if (MarkerIndex == 1) {
                //Grover's Mill";
                
                MapScreenPanOverlay.SetActive(false);
                volume_slider.SetActive(true);
                if (NarrationTimer < 1000) {
                    if (GameObject.Find("1") != null) GameObject.Find("1").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "⬠ Pentagon";    
                    NarrationTimer = 1000;
                    global_timer = 0;
                    // if (GameObject.Find("1") != null) GameObject.Find("1").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "⬠ Pentagon";
                    PlayAudio(LookupNarration("WarOfTheWorldsCredit"));
                } else if (NarrationTimer < 3200) {
                    if (GameObject.Find("1") != null) GameObject.Find("1").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "□ Grover's"; 
                    NarrationTimer = 3200;
                    PlayAudio(LookupNarration("WarOfTheWorldsRedCross"));
                }
                NarrationIndex = 0;
                Stage = "MapZoom";
                // SubtitlesShadow.SetActive(true);
                // Subtitles.SetActive(true);
            } else if (MarkerIndex == 2 && CampaignIndex == 1) { //and finished previous level
                GameObject.Find("2").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Pearl";
                PlayAudio(CampaignPearl);
                NarrationTimer = 2500;
                NarrationIndex = 0;
                global_timer = 0;
                // SubtitlesShadow.SetActive(true);
                // Subtitles.SetActive(true);
            } else if (MarkerIndex == 3 && CampaignIndex == 2) {
                GameObject.Find("3").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Midway";
                PlayAudio(CampaignMidway);
                // SubtitlesShadow.SetActive(false);
                // Subtitles.SetActive(false);
                global_timer = 0;
                NarrationTimer = 3000;
                NarrationIndex = 0;
            } else if (MarkerIndex == 4 && CampaignIndex == 3) {
                GameObject.Find("4").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Finale";
                PlayAudio(CampaignMidway);
                // SubtitlesShadow.SetActive(false);
                // Subtitles.SetActive(false);
                global_timer = 0;
                NarrationTimer = 2999;                
            } else if (MarkerIndex == 4 && CampaignIndex == 3) {
                GameObject.Find("4").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Finale";
                PlayAudio(CampaignMidway);
                // SubtitlesShadow.SetActive(false);
                // Subtitles.SetActive(false);
                global_timer = 0;
                NarrationTimer = 2999;                
            } 
            else if (MarkerIndex >= 6 && MarkerIndex <= 8) {
                global_timer = 0;
                NarrationTimer = 9999;    
                // SubtitlesShadow.SetActive(false);
                // Subtitles.SetActive(false); 
                // PlayAudio(MultiplayerSelectMusic);
            }
            // OverlayZoomIn.SetActive(true);
            // OverlayZoomOut.SetActive(true);
            // volume_slider.SetActive(false);
        }
    }

    public void EnableJoystick() {
        InputJoystick.SetActive(true);
    }
    public void SetEarth() {
        CampaignIntroAssets.SetActive(false);
        CampaignIntroSatelliteAssets.SetActive(false);
        Processor.SetActive(false);
        SetBackground(new Color(128/255f, 167/255f, 174/255f));
        MapMarker.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Multiplay";
        NarrationTimer = 1000;
        Stage = "MapInterface";
        InputJoystick.SetActive(false);
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
    void ResetVideo() {
        if (Stage == "MapInterface") GameObject.Find("World").GetComponent<AudioSource>().Stop();
        // SetBackground(new Color(128/255f, 167/255f, 174/255f));
        SetBackground(new Color(150f/255f, 150f/255f, 150f/255f));
        GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().enabled = false;

    }
    public void RenderProcess() {
        if (InputField.text.Contains("▩")) {
            RenderComponent("Process");
        }
    }
    void FixedUpdate()
    {
        if (Stage == "Loading") {
            Stage = "SplashScreen";
            Printer.SetActive(false);
        }
        // print (Stage);
        global_timer += Time.deltaTime;
        // if (TabToggle.text == "▤ TUI") { //InputField.text.Contains("Processor") && 
        //     RenderText(Processor.GetComponent<ProcessorController>().interpreter.ToString());
        // }
        if (printing) {
            if (print_index < GameObject.Find("Example").transform.GetChild(0).GetComponentsInChildren<ComponentController>().Length) {
                if (GameObject.Find("Printer").GetComponent<PrinterController>().print_index == -1) {
                    GameObject.Find("Printer").GetComponent<PrinterController>().print_index = 0;
                    if (component_name == "Printer" || InputField.text.Contains("▦")) RenderComponent("Printer");
                }
                if (print_obj == null) {
                    print_obj = GameObject.Find("Example").transform.GetChild(0).GetComponentsInChildren<ComponentController>()[print_index++].gameObject;
                }
                if (GameObject.Find("Printer").GetComponent<PrinterController>().GoTo(new Vector2(print_obj.transform.position.x, print_obj.transform.position.z))) {
                    print_obj.GetComponent<ComponentController>().Launch();
                    if (component_name == "Printer" || InputField.text.Contains("▦")) RenderComponent("Printer");
                    print_obj = null;
                }
            } else {
                Printer.SetActive(false);
                ClearText();
                PrinterLeft.SetActive(false);
                PrinterRight.SetActive(false);
                PrinterPrint.SetActive(false);                
                Ship.Start();
                OverlayInteractor.UpdateOptions();
                OverlayInteractor.gameObject.SetActive(false);
                MapScreenPanOverlay.SetActive(true);
                // InputJoystick.SetActive(true);
                // InputUseWeapon.SetActive(true);
                printing = false;
                Example.transform.localPosition = new Vector3(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f), 0);
                if (MarkerIndex == 0) {
                    CycleToggle.SetActive(true);
                    BinocularToggle.SetActive(true);
                    InputUseWeapon.SetActive(true);
                    InputJoystick.SetActive(true);
                    // volume_slider.SetActive(false);
        
                    if (MarkerIndex == 0) {
                        InputUseWeapon.transform.Find("Text").GetComponent<Text>().text = "Launch";
                    }
                    if (MarkerIndex == 1) {
                        if (NarrationTimer < 3000) {
                            InputUseWeapon.transform.Find("Text").GetComponent<Text>().text = "Scan";
                        }
                        else {
                            
                            InputUseWeapon.transform.Find("Text").GetComponent<Text>().text = "Fire";
                        }
                    }

                }
            }
        }
        if (queue_audio != "") {
            if (GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().frame != -1) {
                LoadingScreen.SetActive(false);
                if (CycleToggle != null) CycleToggle.SetActive(false);
                if (BinocularToggle != null) BinocularToggle.SetActive(false);
                PlayAudio(LookupNarration(queue_audio));
                queue_audio = "";
                SetVolume();
            }
            else {
                // print ("play" + );
                if (!GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().isPrepared) {
                    print ("Preparing ...");
                    // GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().Prepare();
                }
                else if (!GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().isPlaying) {
                    print ("Playing ...");
                    GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().Play();
                }

                // PlayVideo("WarOfTheWorldsIntro");
            }
        }
        // } else {
            // if (Stage == "MapInterface" || Stage == "MapZoom" || Stage == "MapZoomed") {
            // Wait for user interaction on Splash Screen  && (NarrationTimer < -1 || (NarrationTimer >= 0 && NarrationTimer != 179))
            // if (NarrationTimer > 210) {
                // RenderText(Processor.GetComponent<ProcessorController>().interpreter.ToString());
            // }
            // DISABLED FOR TESTING TODO ENABLE
            if (Stage != "Loading") NarrationTimer += Time.deltaTime;

            // if (Stage == "MapZoom") {
            // } else {
            // Timer.text = FloatToTime(NarrationTimer) + "\t" + System.DateTime.Now.ToString("M/d") + "/1969";// + "\n" + NarrationIndex + ":" + FloatToTime(NarrationTimer);
            // } 
            // if (Stage == "Loading" && (ulong)GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().frame >= GameObject.Find("Video Player").GetComponent<UnityEngine.Video.VideoPlayer>().frameCount - 1) {
            //     PlayVideo("SplashScreen");
            //     Stage = "SplashScreen";
            // }
 
            bool updated = false;
            while (NarrationIndex < Narration.Count && NarrationTimer >= Narration[NarrationIndex].time) {
                NarrationIndex++;
                updated = true;
            }
            // print (NarrationTimer + " - " + NarrationIndex + " : " + Narration[NarrationIndex - 1].text);
            if (Stage != "Loading" && updated && component_name == "" && NarrationIndex < Narration.Count) { // TODO DISABLE
                MapSubtitles(Narration[NarrationIndex - 1].text);
                history = Narration[NarrationIndex - 1].text;
                // if (NarrationIndex == 47) {
                //     // PlayAudio(SplashScreenComplete);
                //     // PlayAudio(NarratorZoomInMap);
                // }
                // if (NarrationIndex == 68 + 26) {
                //     PlayAudio(NarratorAimTheCrosshair);
                // }           
                // // if (NarrationIndex == 91 + 26) {
                // //     PlayAudio(TutorialLookAround);
                // // }        
                // if (NarrationIndex == 113 + 26) {
                //     PlayAudio(TutorialAttackTarget); InputUseWeapon.SetActive(true);
                    
                // }  
                // if (NarrationIndex == 116 + 26) {
                //     PlayAudio(TutorialUseWeapon);

                // }
                // if (NarrationIndex == 128 + 26) {
                //     InputJoystick.SetActive(true);
                // }
                // if (NarrationIndex == 130 + 26) {
                //     PlayAudio(TutorialGetMoving);
                // }
                // if (NarrationIndex == 132 + 26) {
                //     PlayAudio(TutorialThrottle);
                // }
                // if (NarrationIndex == 137 + 26) {
                //     InputJoystick.transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                //     BinocularToggle.SetActive(true);
                //     PlayAudio(TutorialBinocular);
                // }
                // if (NarrationIndex == 142 + 26) {
                //     PlayAudio(TutorialGood2);
                //     BinocularToggle.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                // }
            // }
            // if (NarrationTimer < 60 && TutorialAssets.transform.childCount > 0) {
            //     if (GameObject.Find("0") != null) GameObject.Find("0").GetComponent<SpriteRenderer>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            // }
            // else if (NarrationTimer >= 2 && NarrationTimer < 6) {
            //     if (GameObject.Find("0") != null) GameObject.Find("0").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            // } else if ((NarrationTimer >= 6 && NarrationTimer < 60)) {
            //     if (GameObject.Find("OverlayZoomIn") != null) GameObject.Find("OverlayZoomIn").GetComponent<Image>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            // } else if ((NarrationTimer >= 60 && NarrationTimer < 61)) {
            //     if (GameObject.Find("OverlayZoomIn") != null) GameObject.Find("OverlayZoomIn").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            // }
            // else if (NarrationTimer >= 180 && NarrationTimer < 240) {
            //     GameObject.Find("0").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Printer";
            //     GameObject.Find("0").GetComponent<SpriteRenderer>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            // }
            // else if (NarrationTimer >= 240 && NarrationTimer < 300) { 
            //     PrinterPrint.GetComponent<Image>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            // }
            // else if (NarrationTimer >= 300 && NarrationTimer < 360) {
            //     if (GameObject.Find("OverlayOk") != null) GameObject.Find("OverlayOk").GetComponent<Image>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            //     if (GameObject.Find("OverlayPanUp") != null) GameObject.Find("OverlayPanUp").GetComponent<Image>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            //     if (GameObject.Find("OverlayPanDown") != null) GameObject.Find("OverlayPanDown").GetComponent<Image>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            //     if (GameObject.Find("OverlayPanRight") != null) GameObject.Find("OverlayPanRight").GetComponent<Image>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            //     if (GameObject.Find("OverlayPanLeft") != null) GameObject.Find("OverlayPanLeft").GetComponent<Image>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            // }
            // else if (NarrationTimer >= 360 && NarrationTimer < 600) {
            //     if (GameObject.Find("CycleToggle") != null) GameObject.Find("CycleToggle").GetComponent<Image>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            // }
            // else if (NarrationTimer >= 600 && NarrationTimer < 780) {
            //     if (GameObject.Find("CycleToggle") != null) GameObject.Find("CycleToggle").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            //     InputUseWeapon.GetComponent<Image>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            // }
            // if (NarrationTimer > 0 && NarrationTimer < 780 && TutorialAssets.transform.childCount == 2) {
            //     InputUseWeapon.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            //     PlayAudio(TutorialHulkDestroyed);
            //     NarrationTimer = 780;
            // }
            // if (NarrationTimer >= 783 && NarrationTimer < 844) {
            //     InputJoystick.transform.GetChild(0).GetComponent<Image>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            // } else if (NarrationTimer >= 844 && NarrationTimer < 880) {
            //     BinocularToggle.GetComponent<Image>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            // } else if (NarrationTimer > 880 && NarrationTimer < 930 && TutorialAssets.transform.childCount == 1) {
            //     PlayAudio(TutorialRightOn);
            //     NarrationTimer = 930;
            // }
            // else if (NarrationTimer > 974 && NarrationTimer < 999) {
            //     Unzoom();
            // }
            // if (NarrationTimer > 0 && NarrationTimer < 960 && TutorialAssets.transform.childCount == 0) {
            //     PlayAudio(TutorialOutro);
            //     NarrationTimer = 960;
            //     BinocularToggle.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            // }
            // if (NarrationTimer > 880 && NarrationTimer < 960)
            // {
            //     if (GameObject.Find("martian_hulk_1") != null) GameObject.Find("martian_hulk_1").GetComponent<SpriteRenderer>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            //     if (GameObject.Find("martian_hulk_2") != null) GameObject.Find("martian_hulk_2").GetComponent<SpriteRenderer>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            //     if (GameObject.Find("martian_hulk_3") != null) GameObject.Find("martian_hulk_3").GetComponent<SpriteRenderer>().color = new Color(.5f + (global_timer * 2) % 1, .5f + (global_timer * 2) % 1, 0, 1f);
            // }

            // /* Campaign Levels End Conditions */
            // if (NarrationTimer > 2200 && NarrationTimer < 2201) {
            //     PlayVideo("Cosmos2");
            //     NarrationTimer = 2201;
            // }
            // if (NarrationTimer > 2000 && NarrationTimer < 2300 && CampaignIntroAssets.transform.GetChild(0).childCount == 0) {
            //     NarrationTimer = 2300;
            //     PlayVideo("Cosmos4");
            //     print ("Level 1 finished");
            //     CampaignIndex = 1;
            //     GameObject.Find("2").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Pearl";
            //     GameObject.Find("3").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "...";
            //     GameObject.Find("4").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "...";
            //     GameObject.Find("5").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "...";
                
            // }
            // if (NarrationTimer > 2400 && NarrationTimer < 2800 && CampaignPearlAssets.transform.GetChild(0).childCount <= 3) {
            //     NarrationTimer = 2800;
            //     PlayVideo("Cosmos5");
            //     print ("Level 2 finished");
            //     CampaignIndex = 2;
            //     GameObject.Find("3").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Midway";
            //     GameObject.Find("4").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "...";
            //     GameObject.Find("5").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "...";
                
            // }
            // if (NarrationTimer > 3000 && NarrationTimer < 4000 && CampaignMidwayAssets.transform.GetChild(0).childCount <= 2) {
            //     NarrationTimer = 4000;
            //     PlayVideo("Cosmos6");
            //     print ("Level 3 finished");
            //     CampaignIndex = 3;
            //     GameObject.Find("4").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Nexus";
            //     GameObject.Find("5").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "...";
                
            // }
            // if (NarrationTimer > 4000 && NarrationTimer < 5000 && CampaignMidwayAssets.transform.GetChild(0).childCount <= 1) {
            //     NarrationTimer = 5000;
            //     PlayVideo("Cosmos6");
            //     print ("Level 4 finished");
            //     CampaignIndex = 4;
            //     GameObject.Find("5").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Abyss";
                
            // }
            // if (NarrationTimer > 5000 && NarrationTimer < 6000 && CampaignMidwayAssets.transform.GetChild(0).childCount == 0) {
            //     NarrationTimer = 6000;
            //     PlayVideo("Cosmos7");
            //     print ("Level 5 finished");
            //     CampaignIndex = 4;
            //     GameObject.Find("5").transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = "Abyss";
            // }
        }
        // MapSubtitlesAtTime("╔═════════════════════╗\n║ BitNaughts Campaign ║\n║   * Report Card *   ║\n╠═════════════════════╣\n║ Time: " + FloatToTime(global_timer) + "\t\t  ║\n╚═════════════════════╝\n\nThanks for playing!", 0f
        // MapSubtitlesAtTime("╔═════════════════════╗\n║ BitNaughts Campaign ║\n║   * Report Card *   ║\n╠═════════════════════╣\n║ Date: " + System.DateTime.Now.ToString("h:mm:ss.f") + "\t  ║\n╚═════════════════════╝\n\nThanks for playing!", 5f
        // MapSubtitlesAtTime("╔═════════════════════╗\n║ BitNaughts Campaign ║\n║   * Report Card *   ║\n╠═════════════════════╣\n║ Date: " + System.DateTime.Now.ToString("MM/dd/yyyy") + "\t  ║\n╚═════════════════════╝\n\nThanks for playing!", 7.5f, story_timer);
        // MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Videos *     ║\n║ Woody Allen's      ║\n║ Radio Days         ║\n║             (1987) ║\n╚════════════════════╝\n\nTap to continue ...", 10f, story_timer);
        // MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Videos *     ║\n║ Jay Bonafield's    ║\n║ The Future Is Now  ║\n║             (1955) ║\n╚════════════════════╝\n\nTap to continue ...", 12.25f, story_timer);
        // MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Videos *     ║\n║ U.S. Navy's        ║\n║ Navigation Satel-  ║\n║ lite System (1955) ║\n╚════════════════════╝\n\nTap to continue ...", 14.5f, story_timer);
        // MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Videos *     ║\n║ U.S. Navy's        ║\n║ Digital Computer   ║\n║ Techniques  (1962) ║\n╚════════════════════╝\n\nTap to continue ...", 16.75f, story_timer);
        // MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Videos *     ║\n║ N.A.S.A's          ║\n║ Space Down to      ║\n║ Earth       (1970) ║\n╚════════════════════╝\n\nTap to continue ...", 19f, story_timer);
        // MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Sprite *     ║\n║ Alejandro Monge's  ║\n║ Modular Spaceships ║\n║             (2014) ║\n╚════════════════════╝\n\nTap to continue ...", 21.25f, story_timer);
        // MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Sound  *     ║\n║ Eidos Interactive  ║\n║ Battlestations     ║\n║ Pacific     (2009) ║\n╚════════════════════╝\n\nTap to continue ...", 23.5f, story_timer);
        // MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n║ Wintergatan's      ║\n║ Valentine          ║\n║             (2013) ║\n╚════════════════════╝\n\nTap to continue ...", 25.75f, story_timer);
        // MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n║ Wintergatan's      ║\n║ Valentine          ║\n║             (2013) ║\n╚════════════════════╝\n\nTap to continue ...", 28f, story_timer);
        // MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n║ Wintergatan's      ║\n║ Valentine          ║\n║             (2013) ║\n\n\nTap to continue ...", 33f, story_timer);
        // MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n║ Wintergatan's      ║\n║ Valentine          ║\n\n\n\nTap to continue ...", 33.5f, story_timer);
        // MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n║ Wintergatan's      ║\n\n\n\n\nTap to continue ...", 34f, story_timer);
        // MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     * Music  *     ║\n\n\n\n\n\nTap to continue ...", 34.5f, story_timer);
        // MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╚════════════════════╝\n\n\n\n\n\n\nTap to continue ...", 35f, story_timer);
        // MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     *   By   *     ║\n║ Brian Hungerman    ║\n║ brianhungerman.com ║\n║             (2022) ║\n╚════════════════════╝\n\nTap to continue ...", 62.25f, story_timer);
        // MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     *   By   *     ║\n║ Brian Hungerman    ║\n║ brianhungerman.com ║\n║             (2022) ║\n\n\nTap to continue ...", 85f, story_timer);
        // MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     *   By   *     ║\n║ Brian Hungerman    ║\n║ brianhungerman.com ║\n\n\n\nTap to continue ...", 85.5f, story_timer);
        // MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     *   By   *     ║\n║ Brian Hungerman    ║\n\n\n\n\nTap to continue ...", 86f, story_timer);
        // MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╠════════════════════╣\n║     *   By   *     ║\n\n\n\n\n\nTap to continue ...", 86.5f, story_timer);
        // MapSubtitlesAtTime("╔════════════════════╗\n║ BitNaughts Credits ║\n╚════════════════════╝\n\n\n\n\n\n\nTap to continue ...", 87f, story_timer);
    }
    void InitializeClickableText(string text, int line, int pos) {
        foreach (var button in ButtonsCache) {
            if (button.activeSelf == false) {
                button.GetComponent<ClickableTextInteractor>().Initialize(this, OverlayInteractor, text, line, pos);
                break;
            }
        }
    }
    public void ProgressCampaign() {
        // Unzoom();
        if (NarrationTimer < 300)  { 
            NarrationTimer = 300;
            PlayAudio(WarOfTheWorldsHeatRay);
        }
    }

    public void MapSpace() {
        SetBackground(new Color(1f/255f, 1f/255f, 36f/255f));
    }

    public void ResetProcessor() {
        print("Reset test");
        Processor.GetComponent<ProcessorController>().interpreter.ResetLine();
    }
}
