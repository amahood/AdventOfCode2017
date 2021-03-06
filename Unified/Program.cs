﻿using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to Advent of Code 2017! Please pass in which Day you would like to evaluate as a command line argument!");
            Console.WriteLine();

            int dayChallenge = Int32.Parse(args[0]);
            if (dayChallenge <1 || dayChallenge > 25)
            {
                Console.WriteLine("Please pass in a valid day challenge between 1 and 25 :(");
                Console.WriteLine();
                return;
            }

            switch (dayChallenge)
            {
                case 1:
                    Day1.TestDay1("181445682966897848665963472661939865313976877194312684993521259486517527961396717561854825453963181134379574918373213732184697746668399631642622373684425326112585283946462323363991753895647177797691214784149215198715986947573668987188746878678399624533792551651335979847131975965677957755571358934665327487287312467771187981424785514785421781781976477326712674311994735947987383516699897916595433228294198759715959469578766739518475118771755787196238772345762941477359483456641194685333528329581113788599843621326313592354167846466415943566183192946217689936174884493199368681514958669615226362538622898367728662941275658917124167353496334664239539753835439929664552886538885727235662548783529353611441231681613535447417941911479391558481443933134283852879511395429489152435996669232681215627723723565872291296878528334773391626672491878762288953597499218397146685679387438634857358552943964839321464529237533868734473777756775687759355878519113426969197211824325893376812556798483325994128743242544899625215765851923959798197562831313891371735973761384464685316273343541852758525318144681364492173465174512856618292785483181956548813344752352933634979165667651165776587656468598791994573513652324764687515345959621493346623821965554755615219855842969932269414839446887613738174567989512857785566352285988991946436148652839391593178736624957214917527759574235133666461988355855613377789115472297915429318142824465141688559333787512328799783539285826471818279818457674417354335454395644435889386297695625378256613558911695145397779576526397241795181294322797687168326696497256684943829666672341162656479563522892141714998477865114944671225898297338685958644728534192317628618817551492975251364233974374724968483637518876583946828819994321129556511537619253381981544394112184655586964655164192552352534626295996968762388827294873362719636616182786976922445125551927969267591395292198155775434997827738862786341543524544822321112131815475829945625787561369956264826651461575948462782869972654343749617939132353399334744265286151177931594514857563664329299713436914721119746932159456287267887878779218815883191236858656959258484139254446341");
                    break;
                case 2:
                    Day2.TestDay2();
                    break;
                case 3:
                    Day3.TestDay3(265149);
                    break;
                case 4:
                    Day4.TestDay4();
                    break;
                case 5:
                    Day5.TestDay5();
                    break;
                case 6:
                    Day6.TestDay6();
                    break;
                case 7:
                    Day7.TestDay7();
                    break;
                case 8:
                    Day8.TestDay8();
                    break;
                case 9:
                    Day9.TestDay9();
                    break;
                case 10:
                    Day10.TestDay10();
                    break;
                case 11:
                    Day11.TestDay11();
                    break;
                case 12:
                    Day12.TestDay12();
                    break;
                case 13:
                    Day13.TestDay13();
                    break;
                case 14:
                    Day14.TestDay14();
                    break;
                case 15:
                    Day15.TestDay15();
                    break;
                case 16:
                    Day16.TestDay16();
                    break;
                case 17:
                    Day17.TestDay17();
                    break;
                case 18:
                    Day18.TestDay18();
                    break;
                case 19:
                    Day19.TestDay19();
                    break;
                case 20:
                    Day20.TestDay20();
                    break;
                 case 21:
                    Day21.TestDay21();
                    break;
                case 22:
                    Day22.TestDay22();
                    break;
                case 23:
                    Day23.TestDay23();
                    break;
                case 24:
                    Day24.TestDay24();
                    break;
                case 25:
                    Day25.TestDay25();
                    break;
                default:
                    break; 
            }

            return;
        }
    }
}
