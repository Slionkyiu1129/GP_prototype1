INCLUDE globals.ink


+{haveGetFlyer != "true"}->noflyer
+{haveGetFlyer == "true" && flowerShopOwnerGetFlyer != "true"} ->haveflyer
+{flowerShopOwnerGetFlyer == "true"} ->alreadyGetFlyer


===noflyer===
#speaker:default #portrait:default 
早安呀！路西安，需要買些花嗎？
本季的玫瑰開得特別漂亮。
#speaker:lucid #portrait:lucid
只是路過看看而已。
#speaker:default #portrait:default
我和花兒們隨時都歡迎你再來光臨。
->END


===haveflyer===
#speaker:default #portrait:default 
哈囉路西安，有什麼事情嗎？
+[給傳單] ->giveFlyer
+[不給傳單] ->dontGiveflyer


===dontGiveflyer===
#speaker:lucid #portrait:lucid
我在找一個人。
#speaker:default #portrait:default 
原來如此，希望你能趕快找到他。
->END


===giveFlyer===
~ flowerShopOwnerGetFlyer = "true"
#speaker:lucid #portrait:lucid
這是芙洛爾的尋人啟事，老闆有看到他嗎？
#speaker:default #portrait:default 
嗯……今天沒有看到耶。
他有時會來店裡問我一些關於花的事情，是個浪漫的人呢。
#speaker:lucid #portrait:lucid
他最近還有來嗎？
#speaker:default #portrait:default 
最近沒什麼看見他。
#speaker:lucid #portrait:lucid
好的謝謝你的資訊。
->END



===alreadyGetFlyer===
#speaker:default #portrait:default 
我還是沒見到芙洛爾，你一定很難過吧，我很遺憾。
->END
