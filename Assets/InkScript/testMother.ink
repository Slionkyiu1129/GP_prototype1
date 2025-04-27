INCLUDE globals.ink


#speaker:芙洛爾媽媽 #portrait:yellow_happy 
你好
->start

=== start ===
你好 #speaker:芙洛爾媽媽 #portrait:yellow_happy
{haveGetFlyer == "": -> flyer}
{haveGetFlyer == "true": ->alreadyGetFlyer}


=== flyer ===
我找不到我的兒子芙洛爾了 #speaker:芙洛爾媽媽 #portrait:yellow_sad
想問你有空幫我把傳單發給其他人嗎 
+[好啊！] ->agreeFlyer
+[對不起沒辦法] ->disAgreeFlyer

=== agreeFlyer ===
~ haveGetFlyer = "true"
~ flyerNum = 10
我其實也很想知道芙洛爾去哪裡了  #speaker:路西 Lucid #portrait:dr_green_sad
謝謝你，要是可以找到芙洛爾就好了 #speaker:芙洛爾媽媽 #portrait:yellow_happy
那給你這 10 張傳單
再麻煩你幫我發完了，謝謝！
->END

=== disAgreeFlyer ===
謝謝你那沒關係 #speaker:芙洛爾媽媽 #portrait:yellow_sad
->END

=== alreadyGetFlyer ===
謝謝你答應幫我發傳單 #speaker:芙洛爾媽媽 #portrait:yellow_happy
再幫我發完剩下的 {flyerNum} 張傳單了
->END


