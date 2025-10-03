INCLUDE globals.ink

+{haveGetFlyer != "true"}->noflyer
+{haveGetFlyer == "true" and TrampGetFlyer!= "true"}->haveflyer

===noflyer===
#speaker:default #portrait:default 
走開 ! 你打擾到我了 ! 
->END

===haveflyer===
#speaker:default #portrait:default 
嘿嘿嘿……死人了……有人死了你知道嗎？不是意外，是被殺的，被殺的啊……
+[給傳單]->giveflyer
+[不給傳單]->dontgiveflyer

===dontgiveflyer===
#speaker:lucid #portrait:lucid
你……在說什麼？
->END

===giveflyer===
~ TrampGetFlyer ="true"
#speaker:default #portrait:default 
血啊，全是血……
#speaker:lucid #portrait:lucid
我不太懂你在說什麼……
#speaker:default #portrait:default 
你不懂？你怎麼會不懂？
#speaker:lucid #portrait:lucid
唉……我只是來發傳單的。這個你拿去，有看到這個人嗎？
#speaker:default #portrait:default 
哦——你送我禮物！那我也要給你禮物！
#speaker:lucid #portrait:lucid
這是什麼？好像讓人有點不舒服……
#speaker:default #portrait:default 
你看！你看這個！還有這個——！
#speaker:lucid #portrait:lucid
這些東西……
#speaker:lucid #portrait:lucid
(這不是芙洛爾的手鍊嗎？這照片……等等，這些怎麼會在他身上？)
#speaker:lucid #portrait:lucid
你這些東西從哪裡來的？
#speaker:default #portrait:default 
你說什麼？你說什麼？！不對！你想搶我東西！你想搶我的寶物！！
#event:NPCExit
#speaker:lucid #portrait:lucid
不是，我只是想問你——
#speaker:lucid #portrait:lucid
該死 ! 
->END


