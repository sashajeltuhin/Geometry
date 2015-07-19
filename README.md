# Geometry
<h2>Social Life of Rectangles</h2>
This application analyzes relative positions of two draggable rectangles. <br>
Here is a list of all possible scenarios:
<ul>
	<li> Rectangles do not share any points.</li>

	<li> Intersection: two rectangles have one or more intersecting lines and produce a result identifying the points of intersection.</li>

	<li> Containment:a rectangle is wholly contained within another rectangle.</li>

	<li> Adjacency: Two rectangles are adjacent. Adjacency is defined as the sharing of a side. Side sharing may be proper or sub-line, 
        where a sub-line share is one where one side of rectangle A is a line that exists as a set of points wholly contained on 
        some other side of rectangle B.</li>

	<li> Rectangles are identical in size and fully overlapping</li>

	<li> Rectangles are partially adjacent, each sharing a sub-line</li>
</ul>
There are two rectangles at play. One is slightly larger than the other by default, but the dimensions of both
shapes can be adjusted in order to demonstrate all permutations of the interaction. The smaller rectangle is always above the larger, so that containment can be shown. The location of
each rectangle can be changed by dragging them arround the grid area. Once the location changes, the application identifies the type of interaction and displays a corresponding message above the 
grid area with the rectangles.

<h3>Demo</h3>
<p>Demo is available <a href="www.mooshpoochini.com">here</a>.

<br>
<h3>Topology and Architecture</h3>
<p>The demonstration is implemented as a web application with a server-side component. The front end is done in HTML, CSS and javascript and relies on Jquery framework for dynamics of the rectangles 
and to communicate with the server component via ajax.<br>
The server facade is an MVC.net application, in which the controller is employed as a service. <br>
The server side has following components:
</p>

<ul>
	<li><b>Geometry.Business</b> - the library, where the algorythms to determine the interactions between the rectangles are implemented</li>
	<li><b>Geometry.DataObjects</b> - the library of business entities used for computation and data transport. The entites are serializable pocos and objects with minimal intrinsic logic
	 The application is working with several different geometrical objects that share a number of common features. These features are captured in an interface and a concrete 
	base class that implements the interface.
	<br>Here is the list of entities used in the aplication:<br>
		<ul>
			<li><b>IShape</b>. The interface with common features</li>
			<li><b>ShapeDO</b>. The concrete base class that implements the IShape interface and holds several base properties</li>
			<li><b>PointDO</b>. The class that encapsulates properties of a point. It is used is a minimal building block for other shapes</li>
			<li><b>RectangleDO</b>. The class that encapsulates properties of a rectangle. Inherits Shape. Contains PointDOs</li>
			<li><b>LineDO</b>. The class that encapsulates properties of a line. Inherits Shape. Contains PointDOs. 
			LineDO is necessary because it can be a product of the adjacency</li>
			<li><b>RelationshipDO</b>. The class that encapsulates the interaction of the rectangles. It contains the information about the interaction as well as
			the shape of the product of the interaction, which may be a rectangle or a line</li>
		</ul>
	</li>
	<li><b>Geometry.Text</b> - the library that provides text strings communicated to the end user. The strings are status messages, error messages and such.
	The encapsulation of text is done for demonstration purposes and convenience only. In real-life scenario it would be connected with a sta repository,
	where the strings can be stored and maintained.<br>
	The library also contains <b>GeometryException</b> - a specialized exception class used in the application.</li>
</ul>

<br>
<br>
<h3>Logic</h3>
<p>
To identify the type of interaction of the rectangles,  the application first determines which vertices of the smaller rectangle (if the diffirence in size is established) are 
located within the bounds of the other rectangle. Then, based on the number of the contained vertices and their position (within or on the border), the application establishes the type
of interaction. In 70% of scenarios, the application can deliver the verdict at this stage. To account for the remainder of scenarios, the application repeats the initial operation but this time
looking for vertices of the second rectangle that fall within the bounds of the first one. Once the type of interaction is established, 
the application determines the coordinates of the overlapping zone in the case of Intersction and Adjacency. The result can be either a rectangle or a line.<br>
The results of the analysis are delivered to the front end, where the interaction type with the corresponding message is communicated to the end-user and the intersecting area (if any) is
shown in red.
</p>


