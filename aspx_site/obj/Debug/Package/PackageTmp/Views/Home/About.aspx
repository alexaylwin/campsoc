<%@ Page Language="C#" MasterPageFile="~/Views/Shared/PublicSite.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>About Campus Social</h2>
    <p>
        I'm Alex, and I'm a fifth year undergradate at McMaster University, studying
        Software Engineering. I created Campus Social initially to support and advertise
        McMaster Engineering Society (<a href="http://macengsociety.ca">http://macengsociety.ca</a>) events and programs.
        I've been involved in a lot of different clubs on campus, with a lot of different budgets and needs. The one 
        thing that I found every club had in common was that the most important measure for the success of an event
        was the number of people who came out to it, and that's why event advertising is so important. <br />
        <br />
        It's hard to get the word out about an event to your student population, especially when you're
        working with a small budget and short deadlines. Campus Social is a way to help you advertise
        your club events more effectively, and get more attendance out to your events.
    </p>

    <p>
        To learn more about Campus Social, feel free to expand the sections below and find out
        how it can help you!
    </p>

     <h3 class="expander">Read the obligatory buzzword-filled summary and sales pitch...<hr /></h3>
    <div class="collapsible" id="1" style="display:none">
        Campus Social is a service to help student clubs, societies and fraternaties 
        promote events and advertise specifically to their audience. It offers entirely 
        customizable features so that your student club can have it&#39;s unique needs met 
        and you continue to improve upon event attendance and involvement in your club.<br />
        <br />
        Most importantly, Campus Social is CHEAP! As a service designed by students and 
        for students, we understand that club budgets are often small. With this in 
        mind, we can work to meet YOUR budget constraints. Other app developers can 
        charge upwards of $3000 for an app with no support. I can do it for $300 (or 
        maybe less!)<br />
        <br />
        It was started by Alex (me!) as a free app for the McMaster Engineering Society 
        (MES), and is now being offered to other campus clubs and societies. I've been
        involved in a number of differnt clubs across campus, with budgets ranging from
        $1000 to $35,000 and have run events for 20 attendee's as well as 1400 attendee's.
     </div>

    <h3 class="expander">So without the buzzwords and bull, what is Campus Social?<hr /></h3>
    <div class="collapsible" id="2" style="display:none">
        Okay, Campus Social offers three different services to help your student club 
        connect with students:<br />
        <br />
        <strong>1. Most importantly, a branded and customized app for your club.</strong> 
        That&#39;s right, your very own app! It can be used to advertise events, send 
        reminders for club members, send messages, get event feedback and anything else 
        that you feel your club needs to be successful. Each app is built for the specific
        club, and can include any features you feel are important.
        <br />
        <br />
        <strong>2. A centralized hub for advertising events online.</strong> You can use 
        Campus Social to send updates to all your standard social media sites (read: 
        Facebook and Twitter) and if a lot of your users are using something else, let 
        us know. We can take a look at the site, and build a plug-in to get that working 
        for you as well. <strong>We can also build a plugin for your website so you can 
        create events from there.</strong><br />
        <br />
        <strong>3. Direct feedback from your club members, and event 
        statistics.</strong> We believe that the key to running good events is knowing 
        what your attendees thought about the last one. With simple user surveys built 
        right into the app, its trivial for users to submit feedback and let you know 
        what they want to see.<br />
        <br />
    </div>

    <h3 class="expander">Alright, how much does it actually cost?<hr /></h3>
    <div class="collapsible" id="3" style="display:none"> 
        The cost depends on what exactly you need. <strong>There is a yearly rate for 
        access to the website</strong>, serving app updates, and so on. That&#39;s $25/year 
        or if you have more than 150 users, its $0.20/user/year, calculated at the 
        renewal of the contract. So if you signed up in August 2012, it would be $25 
        until August 2013. If you wanted to sign up for another year and had 180 
        registered users in August 2013, it would be $36 until August 2014.
        <br />
        <br />
        The higher cost is a one-time fee for building the app. This largely depends
        on the features you want and the amount of customization you need. In general,
        this will range somewhere between $200-$1000, based on what you're looking for.
        <strong>Any budget can be met, and I can work to customize something that will suit 
        your budget restraints.</strong>
        <br />
        <br />
    </div>

    <h3 class="expander">Who makes Campus Social, and why should I care?<hr /></h3>
    <div class="collapsible" id="4" style="display:none;">
        I&#39;m Alex, an undergrad at McMaster University, located in Hamilton Ontario, finishing up 
        my final year of Software Engineering. I&#39;ve been involved in planning, 
        running, and being a general member of a number of clubs across campus, and I&#39;ve 
        held a few positions on the McMaster Engineering Society (MES) council. <strong>
        Most importantly, I&#39;ve been the MES Communications Coordinator for the last 
        three years</strong>. My job has been to manage our IT services, including event 
        advertising on our Facebook/Twitter accounts, as well as on our website and am 
        responsible for maintaining our email lists and services. <strong>I&#39;ve also been 
        involved in planning events for our Frosh Week and for the Software Engineering 
        club, so I know how hard and important it is to promote events and get 
        attendance at events.</strong><br />
        <br />
        This whole project started from a simple app I made for the McMaster Engineering 
        Society and advertised during Frosh Week. With it&#39;s success in our society, I 
        decided to make it available to other student groups as well.<strong> If you 
        ever want to get in touch with me, send me an email at </strong>
        <a href="mailto:AlexAylwin@gmail.com"><strong>AlexAylwin@gmail.com</strong></a><strong> 
        (my personal email) or Alex@campsoc.com!</strong></div>

    <script src="../../Scripts/slideFade.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(".expander").click(function () {
            $(this).next().slideToggle(100);
        });
    </script>
</asp:Content>
