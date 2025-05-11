INCLUDE globals.ink


#speaker:阿鯨船長 #portrait:fishman 
-> start

=== start ===
你好 #speaker:阿鯨船長 #portrait:fishman
+{have_talk_fishman == ""}[給尋人啟示]-> flyer 
+{have_talk_fishman == "true"}[給尋人啟示] -> alreadyTalkFirst
+[不對話了]->ENDDiolog

=== flyer ===
~ have_talk_fishman = "true"
你好，我現在有尋人啟示的單子要給你 #speaker:路西 Lucid #portrait:lucid
什麼？！你說啥，我現在很忙啦？ #speaker:阿鯨船長 #portrait:fishman 
等我整理完漁貨你再跟我說你要幹嘛
你如果真的很急的話，就幫我整理漁貨
->END

=== ENDDiolog ===
再見
->END

=== alreadyTalkFirst ===
要給就先幫我整理漁貨 #speaker:阿鯨船長 #portrait:fishman
我空不出手來
->END