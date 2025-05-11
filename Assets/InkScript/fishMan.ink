INCLUDE globals.ink


#speaker:fishman #portrait:fishman 
-> start

=== start ===
你好 #speaker:fishman #portrait:fishman
+{have_talk_fishman == ""}[給尋人啟示]-> flyer 
+{have_talk_fishman == "true"}[給尋人啟示] -> alreadyTalkFirst
+[不對話了]->ENDDiolog

=== flyer ===
~ have_talk_fishman = "true"
你好，我現在有尋人啟示的單子要給你 #speaker:lucid #portrait:lucid
什麼？！你說啥，我現在很忙啦？ #speaker:fishman #portrait:fishman 
等我整理完漁貨你再跟我說你要幹嘛 
你如果真的很急的話，就幫我整理漁貨 
->END

=== ENDDiolog ===
再見
->END

=== alreadyTalkFirst ===
要給就先幫我整理漁貨 #speaker:fishman #portrait:fishman
我空不出手來
->END