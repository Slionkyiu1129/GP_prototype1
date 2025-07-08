// VAR have_Dialog = false

// LONDON, 1872
// Residence of Monsieur Phileas Fogg.
-> test
// +{have_Dialog == false}-> test
// +{have_Dialog == true}->see


=== test ===
// ~have_Dialog = true
你要檢查什麼  #speaker:doctor #portrait:dr_green_neutral #layout:right
+ [不檢查了]
    -> test_end
* 手臂<>
    沒什麼事
    -> test
* 角<>
    沒什麼事
    -> test
* 頭<>
    沒什麼事
    -> test

    
=== test_end ===
結束檢查 #portrait:dr_green_happy #layout:right
    -> see
    
=== see ===
去哪裡看看 #speaker:??? #portrait:default #layout:left
+[不看了]
    沒什麼好看的
    -> end_see
+[看醫院]
    -> hospital
+[看公園]
    沒什麼好看的
    -> see
+[看學校]
    沒什麼好看的
    -> see

    
=== hospital ===
走到了醫院大廳
 *{not doctor && not goWrite} [這是我第一次看醫生]
    ->goWrite
 +{doctor} [去看醫生] ->doctor
 +{doctor && goWrite} [我要回家]
     ->home

=== goWrite ===
你好，第一次來寫個資料 #speaker:doctor #portrait:dr_green_neutral #layout:right
寫好資料去看醫生
+[走到診室]
->doctor

=== doctor ===
哈囉醫生  #speaker:??? #portrait:default  #layout:left
 +[我不看醫生了] ->hospital
 
=== home ===
你{上看|下看|->inHome}了下門
+[再看] -> home

=== inHome ===
進了家
-> start_npc

=== start_npc ===
{npc: 我不想再理小蜜了->END|這裡好像有人，他是誰去看看} #speaker:??? #portrait:default  #layout:left
+[靠近那個人] #speaker:??? #portrait:default  #layout:left ->npc



=== npc ===
我是小蜜 #speaker:mi #portrait:yellow_happy #layout:right
+ [你在這裡做啥] {~"啦啦"|"嘻嘻"|"呵呵"} 
    -> npc
+ [不理你了] #speaker:??? #portrait:default  #layout:left -> start_npc
     
=== end_see ===
結束看看 #speaker:??? #portrait:default  #layout:left

// === london ===
// Monsieur Phileas Fogg returned home early from the Reform Club, and in a new-fangled steam-carriage, besides!  
// "Passepartout," said he. "We are going around the world!"

// * "Around the world, Monsieur?"
//     I was utterly astonished. 
//     -> astonished
// * [Nod curtly.] -> nod


// === astonished ===
// "You are in jest!" I told him in dignified affront. "You make mock of me, Monsieur."
// "I am quite serious."

// + "But of course"
//     -> ending
// + "But of course2"
//     -> ending


// === nod ===
// I nodded curtly, not believing a word of it.
// -> ending


// === ending
// "We shall circumnavigate the globe within eighty days." He was quite calm as he proposed this wild scheme. "We leave for Paris on the 8:25. In an hour."
-> END