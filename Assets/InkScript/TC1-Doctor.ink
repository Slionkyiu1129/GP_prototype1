INCLUDE globals.ink

+{haveGetFlyer != "true"}->noflyer
+{haveGetFlyer == "true" and DoctorGetFlyer!= "true"}->haveflyer
+{DoctorGetFlyer == "true"}->alreadygetflyer

===noflyer===
#speaker:default #portrait:default 
路西安？你怎麼出現在這裡呢？
#speaker:lucid #portrait:lucid
你好，醫生。只是隨便走走。
->END

===haveflyer===
#speaker:default #portrait:default 
你看起來心事重重的，發生什麼事了？
+[給傳單]->giveflyer
+[不給傳單]->dontgiveflyer

===dontgiveflyer===
#speaker:lucid #portrait:lucid
沒什麼，只是在找人……
#speaker:default #portrait:default 
是村子裡的誰嗎？如果需要幫忙，隨時可以來找我。
->END

===giveflyer===
~ DoctorGetFlyer ="true"
#speaker:lucid #portrait:lucid
這是芙洛爾的尋人啟事，醫生有看到他嗎？
#speaker:default #portrait:default 
？！
#speaker:default #portrait:default 
芙洛爾失蹤了？這太突然了……我會幫忙注意的。
#speaker:default #portrait:default 
對了，你最近還會看見「奇怪的東西」嗎？
#speaker:lucid #portrait:lucid
有時候……
#speaker:default #portrait:default 
那我教你一個減緩不適的方法吧，深呼吸……
->END

===alreadygetflyer===
#speaker:default #portrait:default 
我還沒收到什麼消息，但我會繼續留意，有進展會通知你的。
->END