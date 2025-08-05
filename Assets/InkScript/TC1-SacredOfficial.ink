INCLUDE globals.ink

+{haveGetFlyer != "true"}->noflyer
+{haveGetFlyer == "true" and SacredOfficialGetFlyer!= "true"}->haveflyer
+{SacredOfficialGetFlyer == "true"}->alreadygetflyer

===noflyer===
#speaker:default #portrait:default 
小朋友來這裡做什麼，後山最近有重要的祭祀活動，請不要靠近。
->END

===haveflyer===
#speaker:default #portrait:default 
有什麼事嗎？
+[給傳單]->giveflyer
+[不給傳單]->dontgiveflyer

===dontgiveflyer===
#speaker:lucid #portrait:lucid
沒什麼，只是在閒逛。
->END

===giveflyer===
~ SacredOfficialGetFlyer ="true"
#speaker:lucid #portrait:lucid
這是尋人啟事，請問你這幾天有看過傳單上這個人嗎？
#speaker:default #portrait:default 
我看看……
#speaker:default #portrait:default 
我很抱歉，印象中沒見過，我會拿給我的同事們看看的。
#speaker:lucid #portrait:lucid
沒關係，謝謝你的協助 !
->END

===alreadygetflyer===
希望他沒出什麼事。
->END
