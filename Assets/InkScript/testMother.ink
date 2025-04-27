INCLUDE globals.ink


#speaker:芙洛爾媽媽 #portrait:yellow_happy #layout:right
-> start

=== start ===
你好 #speaker:芙洛爾媽媽 #portrait:yellow_happy #layout:right
+{flyerNum == 10}[給尋人啟示]-> flyer 
+{flyerNum < 10}[給尋人啟示] ->alreadyGetFlyer
+[不對話了]->END


=== flyer ===
我找不到我的兒子芙洛爾了
可以幫我把傳單發給其他人嗎
+[好] ->agreeFlyer
+[抱歉了我好像沒辦法] ->disAgreeFlyer

=== agreeFlyer ===
謝謝你！
那給你這十張傳單
->END

=== disAgreeFlyer ===
謝謝你那沒關係
->END

=== alreadyGetFlyer ===
感謝你答應幫我發傳單了
幫我把你手上剩下的{flyerNum}張傳單發完了
->END


