INCLUDE globals.ink


#speaker:芙洛爾媽媽 #portrait:yellow_happy 
-> start

=== start ===
你好 #speaker:芙洛爾媽媽 #portrait:yellow_happy
{flyerNum == 10: -> flyer}
{flyerNum < 10: ->alreadyGetFlyer}


=== flyer ===
我找不到我的兒子芙洛爾了 #speaker:芙洛爾媽媽 #portrait:yellow_neutral
可以幫我把傳單發給其他人嗎  #speaker:路西 Lucid #portrait:dr_green_neutral
+[好啊] ->agreeFlyer
+[對不起] ->disAgreeFlyer

=== agreeFlyer ===
我其實也很想知道芙洛爾去哪裡了
謝謝你！#speaker:芙洛爾媽媽 #portrait:yellow_neutral
那給你這十張傳單
->END

=== disAgreeFlyer ===
謝謝你那沒關係 #speaker:芙洛爾媽媽 #portrait:yellow_neutral
->END

=== alreadyGetFlyer ===
感謝你答應幫我發傳單 #speaker:芙洛爾媽媽 #portrait:yellow_neutral
幫我把你手上剩下的{flyerNum}張傳單發完了
->END


