INCLUDE globals.ink


// <color=\#bd6d68>你好</color> #speaker:lucid #portrait:lucid
// 測試用文字：如要變更偏好的搜尋主題，請修改搜尋設定。你選擇的主題會儲存在每台已登入 Google 帳戶的電腦上。
+{haveGetFlyer != "true"}->noflyer
+{haveGetFlyer == "true" && have_talk_fishman == ""}[給尋人啟示]-> flyer 

+{haveGetFlyer == "true" &&fishPuzzleFinished == "yes" && have_talk_fishman_ApassPuzzle == ""}[給尋人啟示] -> passPuzzle
+{haveGetFlyer == "true" && fishPuzzleFinished == "yes" && have_talk_fishman_ApassPuzzle == "true"}[給尋人啟示] -> passPuzzleTalkAready
//+{fishPuzzleFinished == "yes"}[有通關了] -> ENDDiolog
//+{fishPuzzleFinished == ""}[還是初始值] -> ENDDiolog
+[沒事～]->ENDDiolog
===noflyer===
#speaker:fishman #portrait:fishman 
嘿吚！！！吼──！！！！！
#speaker:lucid #portrait:lucid
！！
#speaker:fishman #portrait:fishman 
我現在很忙！小朋友去旁邊玩，不要打擾大人工作！
#speaker:lucid #portrait:lucid
啊啊、好的。
#speaker:lucid #portrait:lucid
（聲音好大……）
-> END
=== flyer ===
~ have_talk_fishman = "true"
#speaker:fishman #portrait:fishman 
嘿吚呀！俺現在很忙，如果你真的有事找俺，先幫俺把漁獲整理好再說！
#speaker:lucid #portrait:lucid
嗚哇、那好吧
#speaker:lucid #portrait:lucid
（好大聲……）
->END

=== ENDDiolog ===
再見 #speaker:fishman #portrait:fishman 
->END



=== passPuzzle ===
~ have_talk_fishman_ApassPuzzle = "true"
~ flyerNum = flyerNum-1
 #speaker:fishman #portrait:fishman
 哇哈哈！謝謝你的幫忙，不然原本要花好多時間呢～
 #speaker:fishman #portrait:fishman
 對了！你找俺有什麼事嗎？
 +[給傳單]->giveflyer
+[不給傳單]->dontgiveflyer

-> END
===giveflyer===
 #speaker:lucid #portrait:lucid
 這是芙洛爾的尋人啟事，你有看到他嗎？
 #speaker:fishman #portrait:fishman
 芙洛爾小兄弟啊……他之前偶爾會來碼頭坐著發呆，最近沒見著了。
 #speaker:lucid #portrait:lucid
 啊……這樣啊……
 #speaker:fishman #portrait:fishman
 別洩氣，咱們都自己人，小兄弟如果需要我幫忙，儘管說哈！嘿吚吼！
 -> END
===dontgiveflyer===
 #speaker:lucid #portrait:lucid
 我在找一個人……
 #speaker:fishman #portrait:fishman
 這村子就這點大，要找人應該不難。小兄弟你一定可以的，哇哈哈！
 #speaker:lucid #portrait:lucid
 嗯……
 -> END
=== passPuzzleTalkAready ===
#speaker:fishman #portrait:fishman
芙洛爾的事俺還是沒頭緒，要是找到了一定會讓小兄弟知道的，別給自己太大壓力哈。
->END