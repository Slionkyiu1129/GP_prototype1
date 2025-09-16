INCLUDE globals.ink

->birthday

===birthday===
#speaker:default #portrait:default 
祝你生日快樂~ 我可是特地練習了歌聲喔，不要笑我唱走音！
#speaker:lucid #portrait:lucid
（這好像是去年我生日，Fluor幫我慶生的時候……那天真的很開心。）
#speaker:default #portrait:default 
快許願吧！雖然我沒準備蛋糕，但這小小的點心就當作是蠟燭了。吹掉它，許一個願望！
#speaker:lucid #portrait:lucid
……好。希望我們能一直這樣在一起。
#speaker:default #portrait:default 
哇，好害羞的願望，不過我很喜歡！
#speaker:default #portrait:default 
那麼——路西！我們來試試看壽星會不會運氣比較好！來猜拳吧。

-> play_janken

=== play_janken ===
#speaker:lucid #portrait:lucid
剪刀、石頭、布！

+ [剪刀] 
    -> win
+ [石頭] #speaker:default #portrait:default 
    欸欸！怎麼這樣啦，不算不算！我們再來一次！
    -> play_janken
+ [布] #speaker:default #portrait:default 
    欸欸！怎麼平手啦，不算不算！我們再來一次！
    -> play_janken

=== win ===
#speaker:default #portrait:default 
哇！你贏了欸！壽星果然有特別的運氣！
#speaker:default #portrait:default 
對了，差點忘記——記得去看看噴泉，那裡有我給你的禮物。
->END
