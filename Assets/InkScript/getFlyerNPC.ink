INCLUDE globals.ink


#speaker:npc #portrait:yellow_happy 
-> start

=== start ===
你好 #speaker:npcTest #portrait:yellow_happy
+{have_talk_npcTest == ""}[給尋人啟示]-> flyer 
+{have_talk_npcTest == "true"}[給尋人啟示] -> alreadyTalkFirst
+[不對話了]->ENDDiolog


=== flyer ===
~ have_talk_npcTest = "true"
~ flyerNum = flyerNum-1
好的，我會幫你留意傳單上面的人的！
->END

=== alreadyTalkFirst ===
好的，我會幫你留意傳單上面的人的！
->END

=== ENDDiolog ===
再見！
->END
