INCLUDE globals.ink


#speaker:lucid #portrait:lucid
-> start

=== start ===
你好 #speaker:lucid #portrait:lucid
+{haveGetFlyer == "true" && have_talk_fishman == ""}[給尋人啟示]-> flyer 
+{fishPuzzleFinished == "" && haveGetFlyer == "true" && have_talk_fishman == "true"}[給尋人啟示] -> alreadyTalkFirst
+{haveGetFlyer == "true" &&fishPuzzleFinished == "yes" && have_talk_fishman_ApassPuzzle == ""}[給尋人啟示] -> passPuzzle
+{haveGetFlyer == "true" && fishPuzzleFinished == "yes" && have_talk_fishman_ApassPuzzle == "true"}[給尋人啟示] -> passPuzzleTalkAready
//+{fishPuzzleFinished == "yes"}[有通關了] -> ENDDiolog
//+{fishPuzzleFinished == ""}[還是初始值] -> ENDDiolog
+[沒事～]->ENDDiolog

=== flyer ===
~ have_talk_fishman = "true"
你好，我現在有尋人啟示的單子要給你 #speaker:lucid #portrait:lucid
什麼？！你說啥，我現在很忙啦？ #speaker:fishman #portrait:fishman 
等我整理完漁貨你再跟我說你要幹嘛 
你如果真的很急的話，就幫我整理漁貨 
->END

=== ENDDiolog ===
再見 #speaker:fishman #portrait:fishman 
->END

=== alreadyTalkFirst ===
要給就先幫我整理漁貨 #speaker:fishman #portrait:fishman
我空不出手來
->END


=== passPuzzle ===
~ have_talk_fishman_ApassPuzzle = "true"
~ flyerNum = flyerNum-1
謝謝你幫我整理漁貨 #speaker:fishman #portrait:fishman
你剛剛說什麼再說一次？
我有一張尋人啟事要給你～ #speaker:lucid #portrait:lucid
好！那我再幫你注意上面的人！ #speaker:fishman #portrait:fishman
-> END

=== passPuzzleTalkAready ===
你已經給我過了！我會再幫你注意上面的人！ #speaker:fishman #portrait:fishman
->END
