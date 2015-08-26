# TheBraverest
An application devoted to the Rito API challenge 2.0 targeting the item set category

## Live Link

[TheBraverest.com](http://TheBraverest.com)
<br />
[TheBraverest.com API Documentation](http://TheBraverest.com/Help)

## What is it?

[The Braverest](http://TheBraverest.com) is the next evolution of the League of Legends 
unofficial game mode [Ultimate Bravery](http://www.ultimate-bravery.com/).  

[The Braverest](http://TheBraverest.com) not only picks a super speshul champion and
build for you, it also creates an item set file that you can use to enhance your 
in-game experience by showing you exactly what you need to buy and in what order.

Why be [The Braverest](http://TheBraverest.com) summoner around? 
Because ~~trolling~~ being innovative shouldn't be hard!

## Technical Mumbo Jumbo to make us sound smart

This project is an ASP.NET MVC 5 website that also utilizes Web API 2.

Behind the scenes we call to the [Rito API](https://developer.riotgames.com/api/methods) 
to get our data that we randomly display to the users.

## Setup

You only need to configure 2 things to use this project:
* [Enter the API key you get from Rito here.](https://github.com/neopong/TheBraverest/blob/master/TheBraverest/TheBraverest/Web.config#L20) All data is cached for a day so even a temporary developer key should work.
* [Enter the temp directory you want to write files to here.](https://github.com/neopong/TheBraverest/blob/master/TheBraverest/TheBraverest/Web.config#L19)

## The Team

Scott Karbel 
<br />
	Summoner Name: neopong 
<br />
	Github: [neopong](https://github.com/neopong)
<br />
	LinkedIn: [Scott Karbel](https://www.linkedin.com/in/scottkarbel)

Tyler Thomas 
<br />
	Summoner Name: MMMOverkill 
<br />
	Github: [tt9](https://github.com/tt9)
<br />
	LinkedIn: [Tyler Thomas](https://www.linkedin.com/in/tylerwthomas)