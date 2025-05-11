INCLUDE globals.ink


#speaker:mother #portrait:default 
你好
->start

=== start ===
你好 #speaker:mother #portrait:default
{haveGetFlyer == "": -> flyer}
{haveGetFlyer == "true": ->alreadyGetFlyer}


=== flyer ===
我找不到我的兒子芙洛爾了 #speaker:mother #portrait:default
想問你有空幫我把傳單發給其他人嗎 
+[好啊！] ->agreeFlyer
+[對不起沒辦法] ->disAgreeFlyer

=== agreeFlyer ===
~ haveGetFlyer = "true"
~ flyerNum = 10
我其實也很想知道芙洛爾去哪裡了  #speaker:lucid #portrait:lucid
謝謝你，要是可以找到芙洛爾就好了 #speaker:mother #portrait:default
那給你這 10 張傳單
再麻煩你幫我發完了，謝謝！
->END

=== disAgreeFlyer ===
謝謝你那沒關係 #speaker:mother #portrait:default
->END

=== alreadyGetFlyer ===
謝謝你答應幫我發傳單 #speaker:mother #portrait:default
再幫我發完剩下的 {flyerNum} 張傳單了
->END


