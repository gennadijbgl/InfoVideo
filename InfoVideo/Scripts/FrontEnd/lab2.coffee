class Planet
	constructor: (@name,@satellite) ->
	isMoreSatellites: (count)->
		count <= @satellite
	toString: ->
		@name + ' has ' + @satellite + '\n'

Planet1 = new Planet "Pluton",1
Planet2 = new Planet "Earth",1
Planet3 = new Planet "Neptune",2
Planet4 = new Planet "Uran",14
Planet5 = new Planet "Jupiter",16
Planet6 = new Planet "Saturn",17

SolarSystem = [Planet1, Planet2, Planet3, Planet4,Planet5, Planet6]

do_planets = ->
	str = ''
	satellite_count = $("#count").val()
	SolarSystem.filter (o)->
		o.isMoreSatellites(satellite_count)
	.forEach (o)-> 
		str += o.toString()
	alert(str)

Max = (t,isGreat)->
	item = t[0]
	t.forEach (o)->
		if o > item and isGreat
			item = o 
		else if o < item and not isGreat
			item = o 
	item


MaxWord = (t)->
	item = t[0]
	t.forEach (o)->
		if o.length > item.length
			item = o 
	item

@flag = false
item = []
check = (w) ->
	@flag = false
	for i in [0..w.length-1]
    	if w[i] == w[i].toUpperCase()
    		@flag = true
    		break
    		return



loops = (w)->
	check w
	if @flag
		@item.push(w)
	return

WordsUpper = (t)->
	t.forEach loops
	return @item	

compare = (a,b)->
	if a > b 
		return 1
	else if a< b
		return -1
	return 0

num_arr = ->
	array = $("#num_arr").val().toString().split(" ").map(Number)
	max_a = Max array, true
	min_a = Max array, false
	array = array.sort compare 
	array = array.filter (o) ->
		Math.abs(o) > 1
	sum = 0
	for i in array by 2 then sum+= parseInt(i)
	alert "Sum each 2 = #{sum}"
	$("#num_arr").val array.join(" ")
	alert "Max = #{max_a}[#{array.indexOf(max_a)}], Min = #{min_a}[#{array.indexOf(min_a)}]"

strDo = ->
	myString = $("#strDo").val().toString()
	myString = myString.replace /[,.!?;:()]/gi, "" 
	arr = myString.split " "
	maxLengthWord = MaxWord arr
	alert WordsUpper arr
	alert "Words count #{arr.length}, MaxLengthWord : #{maxLengthWord}[#{arr.indexOf(maxLengthWord)}]"

line = ->
	x1 = $("#a1").val()
	x2 = $("#a2").val()
	y1 = $("#b1").val()
	y2 = $("#b2").val()
	alert "#{x2-y2}x + #{x1-y1}y + #{x1*y2 - x2*y1} = 0"

doTable = ->
	x = document.createElement "TABLE" 

	$("#tbl").appendChild(x)