var DeskApp = {
    ui: {},
    mobile: {ui: {}},
    dataviz: {ui: {}},
    data: {}
};


if (!DeskApp) {
    DeskApp = {};
}

DeskApp.Class = function() { };

DeskApp.Class.prototype = {



    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppClass = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.Class widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.Class">The DeskApp.Class instance (if present).</returns>
};

$.fn.DeskAppClass = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.Class widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.Color = function() { };

DeskApp.Color.prototype = {




    diff: function() {
        /// <summary>
        /// Computes the relative luminance between two colors.
        /// </summary>
        /// <returns type="Number">The relative luminance.</returns>

    },


    equals: function() {
        /// <summary>
        /// Compares two color objects for equality.
        /// </summary>
        /// <returns type="Boolean">returns true if the two colors are the same. Otherwise, false</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppColor = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.Color widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.Color">The DeskApp.Color instance (if present).</returns>
};

$.fn.DeskAppColor = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.Color widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.Layout = function() { };

DeskApp.Layout.prototype = {




    showIn: function(container,view,transitionClass) {
        /// <summary>
        /// Renders the View element in the element specified by the selector
        /// </summary>
        /// <param name="container" type="String" >The selector of the container in which the view element will be appended.</param>
        /// <param name="view" type="DeskApp.View" >The view instance that will be rendered.</param>
        /// <param name="transitionClass" type="string" >Optional. If provided, the new view will replace the current one with a replace effect.</param>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppLayout = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.Layout widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.Layout">The DeskApp.Layout instance (if present).</returns>
};

$.fn.DeskAppLayout = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.Layout widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.Observable = function() { };

DeskApp.Observable.prototype = {




    bind: function(eventName,handler) {
        /// <summary>
        /// Attaches a handler to an event.
        /// </summary>
        /// <param name="eventName" type="String" >The name of the event.</param>
        /// <param name="handler" type="Function" >A function to execute each time the event is triggered. That function should have a single parameter which will contain any event specific data.</param>

    },


    one: function(eventName,handler) {
        /// <summary>
        /// Attaches a handler to an event. The handler is executed only once.
        /// </summary>
        /// <param name="eventName" type="String" >The name of the event.</param>
        /// <param name="handler" type="Function" >A function to execute each time the event is triggered. That function should have a single parameter which will contain any event specific data.</param>

    },


    trigger: function(eventName,eventData) {
        /// <summary>
        /// Executes all handlers attached to the given event.
        /// </summary>
        /// <param name="eventName" type="String" >The name of the event to trigger.</param>
        /// <param name="eventData" type="Object" >Optional event data which will be passed as an argument to the event handlers.</param>

    },


    unbind: function(eventName,handler) {
        /// <summary>
        /// Remove a previously attached event handler.
        /// </summary>
        /// <param name="eventName" type="String" >The name of the event. If not specified all handlers of all events will be removed.</param>
        /// <param name="handler" type="Function" >The handler which should no longer be executed. If not specified all handlers listening to that event will be removed.</param>

    },


    self: null

};

$.fn.getDeskAppObservable = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.Observable widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.Observable">The DeskApp.Observable instance (if present).</returns>
};

$.fn.DeskAppObservable = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.Observable widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.Router = function() { };

DeskApp.Router.prototype = {




    start: function() {
        /// <summary>
        /// Activates the router binding to the URL changes.
        /// </summary>

    },


    route: function(route,callback) {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="route" type="String" >The route definition.</param>
        /// <param name="callback" type="Function" >The callback to be executed when the route is matched.</param>

    },


    navigate: function(route,silent) {
        /// <summary>
        /// Navigates to the given route.
        /// </summary>
        /// <param name="route" type="String" >The route to navigate to.</param>
        /// <param name="silent" type="Boolean" >If set to true, the router callbacks will not be called.</param>

    },


    replace: function(route,silent) {
        /// <summary>
        /// Navigates to the given route, replacing the current view in the history stack (like window.history.replaceState or location.replace work).
        /// </summary>
        /// <param name="route" type="String" >The route to navigate to.</param>
        /// <param name="silent" type="Boolean" >If set to true, the router callbacks will not be called.</param>

    },


    destroy: function() {
        /// <summary>
        /// Unbinds the router instance listeners from the URL fragment part changes.
        /// </summary>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppRouter = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.Router widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.Router">The DeskApp.Router instance (if present).</returns>
};

$.fn.DeskAppRouter = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.Router widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;ignoreCase — Boolean (default: true)
    /// &#10;Introduced with Q3 2014. If set to false, the router instance will perform case sensitive match of the url against the defined routes.
    /// &#10;
    /// &#10;pushState — Boolean (default: false)
    /// &#10;If set to true, the router will use the history pushState API.
    /// &#10;
    /// &#10;root — String (default: "/")
    /// &#10;Applicable if pushState is used and the application is deployed to a path different than /. If the application start page is hosted on http://foo.com/myapp/, the root option should be set to /myapp/.
    /// &#10;
    /// &#10;hashBang — Boolean (default: false)
    /// &#10;Introduced in the 2014 Q1 Service Pack 1 release. If set to true, the hash based navigation will parse and prefix the fragment value with !,
/// &#10;which should be SEO friendly, and allows non-prefixed anchor links to work as expected.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.View = function() { };

DeskApp.View.prototype = {




    destroy: function() {
        /// <summary>
        /// Removes the View element from the DOM. Detaches all event handlers and removes jQuery.data attributes to avoid memory leaks. Calls destroy method of any child DeskApp widgets.
        /// </summary>

    },


    render: function(container) {
        /// <summary>
        /// Renders the view contents. Accepts a jQuery selector (or jQuery object) to which the contents will be appended.
/// Alternatively, the render method can be called without parameters in order to retrieve the View element for manual insertion/further manipulation.
        /// </summary>
        /// <param name="container" type="jQuery" >(optional) the element in which the view element will be appended.</param>
        /// <returns type="jQuery">the view element.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppView = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.View widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.View">The DeskApp.View instance (if present).</returns>
};

$.fn.DeskAppView = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.View widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;evalTemplate — Boolean (default: false)
    /// &#10;If set to true, the view template will be treated as DeskApp template and evaluated against the provided model instance.
    /// &#10;
    /// &#10;tagName — String (default: "div")
    /// &#10;The tag used for the root element of the view.
    /// &#10;
    /// &#10;wrap — Boolean (default: true)
    /// &#10;If set to false, the view will not wrap its contents in a root element. In that case, the view element will point to the root element in the template. If false, the view template should have a single root element.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.data.Binder = function() { };

DeskApp.data.Binder.prototype = {




    refresh: function() {
        /// <summary>
        /// Invoked by the DeskApp UI MVVM framework when the bound view model value is changed. The binder should update the UI (HTML element or DeskApp UI widget) to reflect the view model change.
        /// </summary>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppBinder = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.data.Binder widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.data.Binder">The DeskApp.data.Binder instance (if present).</returns>
};

$.fn.DeskAppBinder = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.data.Binder widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.data.DataSource = function() { };

DeskApp.data.DataSource.prototype = {




    add: function(model) {
        /// <summary>
        /// Appends a data item to the data source.
        /// </summary>
        /// <param name="model" type="Object" >Either a DeskApp.data.Model instance or JavaScript object containing the data item field values.</param>
        /// <returns type="DeskApp.data.Model">the data item which is inserted.</returns>

    },


    aggregate: function(value) {
        /// <summary>
        /// Gets or sets the aggregate configuration.
        /// </summary>
        /// <param name="value" type="Object" >The aggregate configuration. Accepts the same values as the aggregate option.</param>
        /// <returns type="Array">the current aggregate configuration.</returns>

    },


    aggregates: function() {
        /// <summary>
        /// Returns the aggregate results.
        /// </summary>
        /// <returns type="Object">the aggregate results. There is a key for every aggregated field.</returns>

    },


    at: function(index) {
        /// <summary>
        /// Returns the data item at the specified index. The index is zero-based.
        /// </summary>
        /// <param name="index" type="Number" >The zero-based index of the data item.</param>
        /// <returns type="DeskApp.data.ObservableObject">the data item at the specified index. Returns undefined if a data item is not found at the specified index.Returns a DeskApp.data.Model instance if the schema.model option is set.</returns>

    },


    cancelChanges: function(model) {
        /// <summary>
        /// Cancels any pending changes in the data source. Deleted data items are restored, new data items are removed and updated data items are restored to their initial state.
        /// </summary>
        /// <param name="model" type="DeskApp.data.Model" >The optional data item (model). If specified only the changes of this data item will be discarded. If omitted all changes will be discarded.</param>

    },


    data: function(value) {
        /// <summary>
        /// Gets or sets the data items of the data source.If the data source is bound to a remote service (via the transport option) the data method will return the service response.
/// Every item from the response is wrapped in a DeskApp.data.ObservableObject or DeskApp.data.Model (if the schema.model option is set).If the data source is bound to a JavaScript array (via the data option) the data method will return the items of that array.
/// Every item from the array is wrapped in a DeskApp.data.ObservableObject or DeskApp.data.Model (if the schema.model option is set).If the data source is grouped (via the group option or the group method) and the serverGrouping is set to true
/// the data method will return the group items.
        /// </summary>
        /// <param name="value" type="Object" >The data items which will replace the current ones in the data source. If omitted the current data items will be returned.</param>
        /// <returns type="DeskApp.data.ObservableArray">the data items of the data source. Returns empty array if the data source hasn't been populated with data items via the read, fetch or query methods.</returns>

    },


    fetch: function(callback) {
        /// <summary>
        /// Reads the data items from a remote service (if the transport option is set) or from a JavaScript array (if the data option is set).
        /// </summary>
        /// <param name="callback" type="Function" >The optional function which is executed when the remote request is finished.  The function context (available via the this keyword) will be set to the data source instance.</param>
        /// <returns type="Promise">A promise that will be resolved when the data has been loaded, or rejected if an HTTP error occurs.</returns>

    },


    filter: function(value) {
        /// <summary>
        /// Gets or sets the filter configuration.
        /// </summary>
        /// <param name="value" type="Object" >The filter configuration. Accepts the same values as the filter option (check there for more examples).</param>
        /// <returns type="Object">the current filter configuration.</returns>

    },


    get: function(id) {
        /// <summary>
        /// Gets the data item (model) with the specified id.
        /// </summary>
        /// <param name="id" type="Object" >The id of the model to look for.</param>
        /// <returns type="DeskApp.data.Model">the model instance. Returns undefined if a model with the specified id is not found.</returns>

    },


    getByUid: function(uid) {
        /// <summary>
        /// Gets the data item (model) with the specified uid.
        /// </summary>
        /// <param name="uid" type="String" >The uid of the model to look for.</param>
        /// <returns type="DeskApp.data.ObservableObject">the model instance. Returns undefined if a model with the specified uid is not found.</returns>

    },


    group: function(value) {
        /// <summary>
        /// Gets or sets the grouping configuration.
        /// </summary>
        /// <param name="value" type="Object" >The grouping configuration. Accepts the same values as the group option.</param>
        /// <returns type="Array">the current grouping configuration.</returns>

    },


    hasChanges: function() {
        /// <summary>
        /// Checks if the data items have changed.
        /// </summary>
        /// <returns type="Boolean">returns true if the data items have changed. Otherwise, false.</returns>

    },


    indexOf: function(dataItem) {
        /// <summary>
        /// Gets the index of the specified data item.
        /// </summary>
        /// <param name="dataItem" type="DeskApp.data.ObservableObject" >The target data item.</param>
        /// <returns type="Number">the index of the specified data item. Returns -1 if the data item is not found.</returns>

    },


    insert: function(index,model) {
        /// <summary>
        /// Inserts a data item in the data source at the specified index.
        /// </summary>
        /// <param name="index" type="Number" >The zero-based index at which the data item will be inserted.</param>
        /// <param name="model" type="Object" >Either a DeskApp.data.Model instance or JavaScript object containing the field values.</param>
        /// <returns type="DeskApp.data.Model">the data item which is inserted.</returns>

    },


    online: function(value) {
        /// <summary>
        /// Gets or sets the online state of the data source.
        /// </summary>
        /// <param name="value" type="Boolean" >The online state - true for online, false for offline.</param>
        /// <returns type="Boolean">the current online state - true if online; otherwise false.</returns>

    },


    offlineData: function(data) {
        /// <summary>
        /// Gets or sets the offline state of the data source.
        /// </summary>
        /// <param name="data" type="Array" >The array of data items that replace the current offline state of the data source.</param>
        /// <returns type="Array">array of JavaScript objects that represent the data items. Changed data items have a __state__ field attached. That field indicates the type of change: "create", "update" or "destroy". Unmodified data items don't have a __state__ field.</returns>

    },


    page: function(page) {
        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <param name="page" type="Number" >The new page.</param>
        /// <returns type="Number">the current page.</returns>

    },


    pageSize: function(size) {
        /// <summary>
        /// Gets or sets the current page size.
        /// </summary>
        /// <param name="size" type="Number" >The new page size.</param>
        /// <returns type="Number">the current page size.</returns>

    },


    pushCreate: function(items) {
        /// <summary>
        /// Appends the specified data item(s) to the data source without marking them as "new". The data source will not sync data items appended via pushCreate.
        /// </summary>
        /// <param name="items" type="Object" >The data item or data items to append to the data source.</param>

    },


    pushDestroy: function(items) {
        /// <summary>
        /// Removes the specified data item(s) from the data source without marking them as "removed". The data source will not sync data items appended via pushDestroy.
        /// </summary>
        /// <param name="items" type="Object" >The data item or data items to remove from the data source.</param>

    },


    pushUpdate: function(items) {
        /// <summary>
        /// Updates the specified data item(s) without marking them as "dirty". The data source will not sync data items appended via pushUpdate.
/// If the data items are not found (using schema.model.id) they will be appended.
        /// </summary>
        /// <param name="items" type="Object" >The data item or data items to update.</param>

    },


    query: function(options) {
        /// <summary>
        /// Executes the specified query over the data items. Makes a HTTP request if bound to a remote service.
        /// </summary>
        /// <param name="options" type="" >The query options which should be applied.</param>
        /// <returns type="Promise">A promise that will be resolved when the data has been loaded, or rejected if an HTTP error occurs.</returns>

    },


    read: function(data) {
        /// <summary>
        /// Reads data items from a remote service (if the transport option is set) or from a JavaScript array (if the data option is set).
        /// </summary>
        /// <param name="data" type="Object" >Optional data to pass to the remote service. If you need to filter, it is better to use the filter() method or the query() method with a filter parameter.</param>
        /// <returns type="Promise">A promise that will be resolved when the data has been loaded, or rejected if an HTTP error occurs.</returns>

    },


    remove: function(model) {
        /// <summary>
        /// Removes the specified data item from the data source.
        /// </summary>
        /// <param name="model" type="DeskApp.data.Model" >The data item which should be removed.</param>

    },


    sort: function(value) {
        /// <summary>
        /// Gets or sets the sort order which will be applied over the data items.
        /// </summary>
        /// <param name="value" type="Object" >The sort configuration. Accepts the same values as the sort option.</param>
        /// <returns type="Array">the current sort configuration.</returns>

    },


    sync: function() {
        /// <summary>
        /// Saves any data item changes.The sync method will request the remote service if:
        /// </summary>
        /// <returns type="Promise">A promise that will be resolved when all sync requests have finished succesfully, or rejected if any single request fails.</returns>

    },


    total: function() {
        /// <summary>
        /// Gets the total number of data items. Uses schema.total if the transport.read option is set.
        /// </summary>
        /// <returns type="Number">the total number of data items. Returns the length of the array returned by the data method if schema.total or transport.read are not set.Returns 0 if the data source hasn't been populated with data items via the read, fetch or query methods.</returns>

    },


    totalPages: function() {
        /// <summary>
        /// Gets the number of available pages.
        /// </summary>
        /// <returns type="Number">the available pages.</returns>

    },


    view: function() {
        /// <summary>
        /// Returns the data items which correspond to the current page, filter, sort and group configuration.To ensure that data is available this method should be used within the change event handler or the fetch method.
        /// </summary>
        /// <returns type="DeskApp.data.ObservableArray">the data items. Returns groups if the data items are grouped (via the group option or the group method).</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppDataSource = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.data.DataSource widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.data.DataSource">The DeskApp.data.DataSource instance (if present).</returns>
};

$.fn.DeskAppDataSource = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.data.DataSource widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;aggregate — Array 
    /// &#10;The aggregate(s) which are calculated when the data source populates with data. The supported aggregates are "average", "count", "max", "min" and "sum".
    /// &#10;
    /// &#10;autoSync — Boolean (default: false)
    /// &#10;If set to true the data source would automatically save any changed data items by calling the sync method. By default changes are not automatically saved.
    /// &#10;
    /// &#10;batch — Boolean (default: false)
    /// &#10;If set to true the data source will batch CRUD operation requests. For example updating two data items would cause one HTTP request instead of two. By default the data source
/// &#10;makes a HTTP request for every CRUD operation.
    /// &#10;
    /// &#10;data — Array|String 
    /// &#10;The array of data items which the data source contains. The data source will wrap those items as DeskApp.data.ObservableObject or DeskApp.data.Model (if schema.model is set).Can be set to a string value if the schema.type option is set to "xml".
    /// &#10;
    /// &#10;filter — Array|Object 
    /// &#10;The filter(s) which is (are) applied over the data items. By default no filter is applied.
    /// &#10;
    /// &#10;group — Array|Object 
    /// &#10;The grouping configuration of the data source. If set the data items will be grouped when the data source is populated. By default grouping is not applied.
    /// &#10;
    /// &#10;offlineStorage — String|Object 
    /// &#10;The offline storage key or custom offline storage implementation.
    /// &#10;
    /// &#10;page — Number 
    /// &#10;The page of data which the data source will return when the view method is invoked or request from the remote service.
    /// &#10;
    /// &#10;pageSize — Number 
    /// &#10;The number of data items per page.
    /// &#10;
    /// &#10;schema — Object 
    /// &#10;The configuration used to parse the remote service response.
    /// &#10;
    /// &#10;serverAggregates — Boolean (default: false)
    /// &#10;If set to true the data source will leave the aggregate calculation to the remote service. By default the data source calculates aggregates client-side.
    /// &#10;
    /// &#10;serverFiltering — Boolean (default: false)
    /// &#10;If set to true the data source will leave the filtering implementation to the remote service. By default the data source performs filtering client-side.By default the filter is sent to the server following jQuery's conventions.For example the filter { logic: "and", filters: [ { field: "name", operator: "startswith", value: "Jane" } ] } is sent as:Use the parameterMap option to send the filter option in a different format.
    /// &#10;
    /// &#10;serverGrouping — Boolean (default: false)
    /// &#10;If set to true the data source will leave the grouping implementation to the remote service. By default the data source performs grouping client-side.By default the group is sent to the server following jQuery's conventions.For example the group { field: "category", dir: "desc" } is sent as:Use the parameterMap option to send the group option in a different format.
    /// &#10;
    /// &#10;serverPaging — Boolean (default: false)
    /// &#10;If set to true the data source will leave the data item paging implementation to the remote service. By default the data source performs paging client-side.The following options are sent to the server when server paging is enabled:Use the parameterMap option to send the paging options in a different format.
    /// &#10;
    /// &#10;serverSorting — Boolean (default: false)
    /// &#10;If set to true the data source will leave the data item sorting implementation to the remote service. By default the data source performs sorting client-side.By default the sort is sent to the server following jQuery's conventions.For example the sort { field: "age", dir: "desc" } is sent as:Use the parameterMap option to send the paging options in a different format.
    /// &#10;
    /// &#10;sort — Array|Object 
    /// &#10;The sort order which will be applied over the data items. By default the data items are not sorted.
    /// &#10;
    /// &#10;transport — Object 
    /// &#10;The configuration used to load and save the data items. A data source is remote or local based on the way of it retrieves data items.Remote data sources load and save data items from and to a remote end-point (a.k.a. remote service or server). The transport option describes the remote service configuration - URL, HTTP verb, HTTP headers etc.
/// &#10;The transport option can also be used to implement custom data loading and saving.Local data sources are bound to a JavaScript array via the data option.
    /// &#10;
    /// &#10;type — String 
    /// &#10;If set the data source will use a predefined transport and/or schema.
/// &#10;The supported values are "odata" which supports the OData v.2 protocol, "odata-v4" which partially supports
/// &#10;odata version 4 and "signalr".
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.data.GanttDataSource = function() { };

DeskApp.data.GanttDataSource.prototype = {




    taskAllChildren: function(task) {
        /// <summary>
        /// Returns a list of all child tasks. The search is recursive.
        /// </summary>
        /// <param name="task" type="DeskApp.data.GanttTask" >The parent task. If this parameter is not specified, all gantt tasks will be returned.</param>
        /// <returns type="Array">the list of all child tasks.</returns>

    },


    taskChildren: function(task) {
        /// <summary>
        /// Returns a list of all direct child tasks.
        /// </summary>
        /// <param name="task" type="DeskApp.data.GanttTask" >The parent task. If this parameter is not specified, all root level tasks will be returned.</param>
        /// <returns type="Array">the list of all direct child tasks.</returns>

    },


    taskLevel: function(task) {
        /// <summary>
        /// Returns the level of the task in the hierrarchy. 0 for root level taks.
        /// </summary>
        /// <param name="task" type="DeskApp.data.GanttTask" >The reference task.</param>
        /// <returns type="Number">the level of the task in the hierarchy.</returns>

    },


    taskParent: function(task) {
        /// <summary>
        /// Returns the parent task of a certain task.
        /// </summary>
        /// <param name="task" type="DeskApp.data.GanttTask" >The reference task.</param>
        /// <returns type="DeskApp.data.GanttTask">the parent task.</returns>

    },


    taskSiblings: function(task) {
        /// <summary>
        /// Returns a list of all tasks that have the same parent.
        /// </summary>
        /// <param name="task" type="DeskApp.data.GanttTask" >The reference task.</param>
        /// <returns type="Array">the list of all tasks with the same parent as the parameter task. If the parameter task is a root level task, all root level tasks are returned.</returns>

    },


    taskTree: function(task) {
        /// <summary>
        /// Returns a list of all child gantt tasks, ordered by their hierarchical index (Depth-First). a parent is collapsed, it's children are not returned.
        /// </summary>
        /// <param name="task" type="DeskApp.data.GanttTask" >The reference task. If this parameter is specified, the result will be all child tasks of this task, ordered by their hierarchical index.</param>
        /// <returns type="Array">the list of all child gantt tasks, ordered by their hierarchical index (Depth-First).</returns>

    },


    update: function(task,taskInfo) {
        /// <summary>
        /// Updates a gantt task.
        /// </summary>
        /// <param name="task" type="DeskApp.data.GanttTask" >The task to be updated.</param>
        /// <param name="taskInfo" type="Object" >The new values, which will be used to update the task.</param>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppGanttDataSource = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.data.GanttDataSource widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.data.GanttDataSource">The DeskApp.data.GanttDataSource instance (if present).</returns>
};

$.fn.DeskAppGanttDataSource = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.data.GanttDataSource widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;schema — Object 
    /// &#10;The schema configuration of the GanttDataSource.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.data.GanttDependency = function() { };

DeskApp.data.GanttDependency.prototype = {



    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppGanttDependency = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.data.GanttDependency widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.data.GanttDependency">The DeskApp.data.GanttDependency instance (if present).</returns>
};

$.fn.DeskAppGanttDependency = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.data.GanttDependency widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;id — String|Number|Object 
    /// &#10;The mandatory unique identifier of the dependency.
    /// &#10;
    /// &#10;predecessorId — String|Number|Object 
    /// &#10;The  mandatory 'id' of the predecessor task.
    /// &#10;
    /// &#10;successorId — String|Number|Object 
    /// &#10;The  mandatory 'id' of the successor task.
    /// &#10;
    /// &#10;type — String|Number|Object 
    /// &#10;The type of the dependency. The type is a value between 0 and 3, representing the four different dependency types: 0 - Finish-Finish, 1 - Finish-Start, 2 - Start-Finish, 3 - Start-Start.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.data.GanttDependencyDataSource = function() { };

DeskApp.data.GanttDependencyDataSource.prototype = {




    dependencies: function(id) {
        /// <summary>
        /// Returns a list of all dependencies for a certain task.
        /// </summary>
        /// <param name="id" type="Object" >The id of the gantt task, based on which the dependencies are filtered.</param>
        /// <returns type="Array">the list of all task dependencies.</returns>

    },


    predecessors: function(id) {
        /// <summary>
        /// Returns a list of all predecessor dependencies for a certain task.
        /// </summary>
        /// <param name="id" type="Object" >The id of the gantt task, based on which the dependencies are filtered.</param>
        /// <returns type="Array">the list of all task predecessors.</returns>

    },


    successors: function(id) {
        /// <summary>
        /// Returns a list of all successor dependencies for a certain task.
        /// </summary>
        /// <param name="id" type="Object" >The id of the gantt task, based on which the dependencies are filtered.</param>
        /// <returns type="Array">the list of all task successors.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppGanttDependencyDataSource = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.data.GanttDependencyDataSource widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.data.GanttDependencyDataSource">The DeskApp.data.GanttDependencyDataSource instance (if present).</returns>
};

$.fn.DeskAppGanttDependencyDataSource = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.data.GanttDependencyDataSource widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;schema — Object 
    /// &#10;The schema configuration of the GanttDependencyDataSource.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.data.GanttTask = function() { };

DeskApp.data.GanttTask.prototype = {




    duration: function() {
        /// <summary>
        /// Returns the gantt task length in milliseconds.
        /// </summary>
        /// <returns type="Number">the length of the task.</returns>

    },


    isMilestone: function() {
        /// <summary>
        /// Checks whether the event has zero duration.
        /// </summary>
        /// <returns type="Boolean">return true if the task start is equal to the task end.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppGanttTask = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.data.GanttTask widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.data.GanttTask">The DeskApp.data.GanttTask instance (if present).</returns>
};

$.fn.DeskAppGanttTask = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.data.GanttTask widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;end — Date 
    /// &#10;The date at which the gantt task ends. The end date is mandatory.
    /// &#10;
    /// &#10;expanded — Boolean (default: true)
    /// &#10;If set to true the task is expanded and it's child tasks are visible.
    /// &#10;
    /// &#10;id — String|Number|Object 
    /// &#10;The mandatory unique identifier of the task.
    /// &#10;
    /// &#10;orderId — String|Number|Object (default: 0)
    /// &#10;The position of the task relative to its sibling tasks.
    /// &#10;
    /// &#10;parentId — String|Number|Object (default: null)
    /// &#10;The 'id' of the parent task. Required for child tasks.
    /// &#10;
    /// &#10;percentComplete — String|Number|Object (default: 0)
    /// &#10;The percentage of completion of the task. A value between 0 and 1, representing how much of a task is completed.
    /// &#10;
    /// &#10;start — Date 
    /// &#10;The date at which the gantt task starts. The start date is mandatory.
    /// &#10;
    /// &#10;summary — Boolean (default: true)
    /// &#10;If set to true the task has child tasks.
    /// &#10;
    /// &#10;title — String (default: "")
    /// &#10;The title of the task which is displayed by the gantt widget.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.data.HierarchicalDataSource = function() { };

DeskApp.data.HierarchicalDataSource.prototype = {



    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppHierarchicalDataSource = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.data.HierarchicalDataSource widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.data.HierarchicalDataSource">The DeskApp.data.HierarchicalDataSource instance (if present).</returns>
};

$.fn.DeskAppHierarchicalDataSource = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.data.HierarchicalDataSource widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;schema — Object 
    /// &#10;The schema configuration. See the DataSource.schema configuration for all available options.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.data.Model = function() { };

DeskApp.data.Model.prototype = {




    bind: function() {
        /// <summary>
        /// Attaches a handler to an event. Examples and more info can be found in the bind section of the DeskApp.Observable API reference.
        /// </summary>

    },


    editable: function(field) {
        /// <summary>
        /// Determines if the specified field is editable or not.
        /// </summary>
        /// <param name="field" type="String" >The field to check.</param>
        /// <returns type="Boolean">true if the field is editable; false otherwise.</returns>

    },


    get: function() {
        /// <summary>
        /// Gets the value of the specified field. Inherited from DeskApp.data.ObservableObject. Examples and more info can be found in the get section of the
/// ObservableObject API reference.
        /// </summary>

    },


    isNew: function() {
        /// <summary>
        /// Checks if the Model is new or not. The id field is used to determine if a model instance is new or existing one.
/// If the value of the field specified is equal to the default value (specified through the fields configuration) the model is considered as new.
        /// </summary>
        /// <returns type="Boolean">true if the model is new; false otherwise.</returns>

    },


    set: function() {
        /// <summary>
        /// Sets the value of the specified field. Inherited from DeskApp.data.ObservableObject. Examples and more info can be found in the set section of the
/// ObservableObject API reference.
        /// </summary>

    },


    toJSON: function() {
        /// <summary>
        /// Creates a plain JavaScript object which contains all fields of the Model. Inherited from DeskApp.data.ObservableObject. Examples and more info can be found in the toJSON section of the
/// ObservableObject API reference.
        /// </summary>

    },


    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppModel = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.data.Model widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.data.Model">The DeskApp.data.Model instance (if present).</returns>
};

$.fn.DeskAppModel = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.data.Model widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.data.Node = function() { };

DeskApp.data.Node.prototype = {




    append: function(model) {
        /// <summary>
        /// Appends a new item to the children data source, and initializes it if necessary.
        /// </summary>
        /// <param name="model" type="Object" >The data for the new item</param>

    },


    level: function() {
        /// <summary>
        /// Gets the current nesting level of the node within the data source.
        /// </summary>
        /// <returns type="Number">the zero based level of the node.</returns>

    },


    load: function() {
        /// <summary>
        /// Loads the child nodes in the child data source, supplying the id of the Node to the request.
        /// </summary>

    },


    loaded: function() {
        /// <summary>
        /// Gets or sets the loaded flag of the Node. Setting the loaded flag to false allows reloading of child items.
        /// </summary>

    },


    parentNode: function() {
        /// <summary>
        /// Gets the parent node.
        /// </summary>
        /// <returns type="DeskApp.data.Node">the parent of the node; null if the node is a root node or doesn't have a parent.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppNode = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.data.Node widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.data.Node">The DeskApp.data.Node instance (if present).</returns>
};

$.fn.DeskAppNode = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.data.Node widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.data.ObservableArray = function() { };

DeskApp.data.ObservableArray.prototype = {




    bind: function(eventName,handler) {
        /// <summary>
        /// Attaches an event handler for the specified event.
        /// </summary>
        /// <param name="eventName" type="String" >The name of the event.</param>
        /// <param name="handler" type="Function" >The function which will be invoked when the event is raised.</param>

    },


    join: function(separator) {
        /// <summary>
        /// Joins all items of an ObservableArray into a string. Equivalent of
/// Array.prototype.join.
        /// </summary>
        /// <param name="separator" type="String" >Specifies the string to separate each item of the array. If omitted the array items are separated with a comma (,)</param>

    },


    parent: function() {
        /// <summary>
        /// Gets the parent of the array if such parent exists.
        /// </summary>
        /// <returns type="DeskApp.data.ObservableObject">the parent of the array; undefined if the array is not nested and doesn't have a parent.</returns>

    },


    pop: function() {
        /// <summary>
        /// Removes the last item from an array and returns that item. Equivalent of Array.prototype.pop.
        /// </summary>
        /// <returns type="Object">the item which was removed.</returns>

    },


    push: function() {
        /// <summary>
        /// Appends the given items to the array and returns the new length of the array. Equivalent of Array.prototype.push.
/// The new items are wrapped as ObservableObject if they are complex objects.
        /// </summary>
        /// <returns type="Number">the new length of the array.</returns>

    },


    slice: function(begin,end) {
        /// <summary>
        /// Returns a one-level deep copy of a portion of an array. Equivalent of
/// Array.prototype.slice.
/// The result of the slice method is not an instance of ObvservableArray. It is a regular JavaScript Array object.
        /// </summary>
        /// <param name="begin" type="Number" >Zero-based index at which to begin extraction.</param>
        /// <param name="end" type="Number" >Zero-based index at which to end extraction. If end is omitted, slice extracts to the end of the sequence.</param>

    },


    splice: function(index,howMany) {
        /// <summary>
        /// Changes an ObservableArray, by adding new items while removing old items. Equivalent of
/// Array.prototype.splice
        /// </summary>
        /// <param name="index" type="Number" >Index at which to start changing the array. If negative, will begin that many elements from the end.</param>
        /// <param name="howMany" type="Number" >An integer indicating the number of items to remove. If set to 0, no items are removed. In this case, you should specify at least one new item.</param>
        /// <returns type="Array">containing the removed items. The result of the splice method is not an instance of ObvservableArray.</returns>

    },


    shift: function() {
        /// <summary>
        /// Removes the first item from an ObvservableArray and returns that item. Equivalent of Array.prototype.shift.
        /// </summary>
        /// <returns type="Object">the item which was removed.</returns>

    },


    toJSON: function() {
        /// <summary>
        /// Returns a JavaScript Array which represents the contents of the ObservableArray.
        /// </summary>

    },


    unshift: function() {
        /// <summary>
        /// Adds one or more items to the beginning of an ObservableArray and returns the new length.  Equivalent of Array.prototype.unshift.
        /// </summary>
        /// <returns type="Number">the new length of the array.</returns>

    },


    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppObservableArray = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.data.ObservableArray widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.data.ObservableArray">The DeskApp.data.ObservableArray instance (if present).</returns>
};

$.fn.DeskAppObservableArray = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.data.ObservableArray widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.data.ObservableObject = function() { };

DeskApp.data.ObservableObject.prototype = {




    bind: function() {
        /// <summary>
        /// Attaches a handler to an event. Examples and more info can be found in the bind section of the
/// DeskApp.Observable API reference.
        /// </summary>

    },


    get: function(name) {
        /// <summary>
        /// Gets the value of the specified field.
        /// </summary>
        /// <param name="name" type="String" >The name of the field whose value is going to be returned.</param>
        /// <returns type="Object">the value of the specified field.</returns>

    },


    parent: function() {
        /// <summary>
        /// Gets the parent of the object if such parent exists.
        /// </summary>
        /// <returns type="DeskApp.data.ObservableObject">the parent of the object; undefined if the object is not nested and doesn't have a parent.</returns>

    },


    set: function(name,value) {
        /// <summary>
        /// Sets the value of the specified field.
        /// </summary>
        /// <param name="name" type="String" >The name of the field whose value is going to be returned.</param>
        /// <param name="value" type="Object" >The new value of the field.</param>

    },


    toJSON: function() {
        /// <summary>
        /// Creates a plain JavaScript object which contains all fields of the ObservableObject.
        /// </summary>
        /// <returns type="Object">which contains only the fields of the ObservableObject.</returns>

    },


    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppObservableObject = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.data.ObservableObject widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.data.ObservableObject">The DeskApp.data.ObservableObject instance (if present).</returns>
};

$.fn.DeskAppObservableObject = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.data.ObservableObject widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.data.PivotDataSource = function() { };

DeskApp.data.PivotDataSource.prototype = {




    axes: function() {
        /// <summary>
        /// Get the parsed axes data
        /// </summary>
        /// <returns type="Object">the parsed axes data</returns>

    },


    catalog: function(name) {
        /// <summary>
        /// Get or sets the current catalog name.
        /// </summary>
        /// <param name="name" type="String" >The name of the catalog.</param>
        /// <returns type="String">the current catalog name.</returns>

    },


    columns: function(val) {
        /// <summary>
        /// Get or sets the columns configuration.
        /// </summary>
        /// <param name="val" type="Array" >The columns configuration. Accepts the same values as the columns option.</param>
        /// <returns type="Array">the current columns configuration.</returns>

    },


    cube: function(name) {
        /// <summary>
        /// Get or sets the current cube name.
        /// </summary>
        /// <param name="name" type="String" >The name of the cube.</param>
        /// <returns type="String">the current cube name.</returns>

    },


    discover: function(options) {
        /// <summary>
        /// Starts discover request with given options.
        /// </summary>
        /// <param name="options" type="String" >The options of the discover request</param>
        /// <returns type="Object">Deferred object</returns>

    },


    measures: function(val) {
        /// <summary>
        /// Get or sets the measures configuration.
        /// </summary>
        /// <param name="val" type="Array" >The measures configuration. Accepts the same values as the measures option.</param>
        /// <returns type="Array">the current measures configuration.</returns>

    },


    measuresAxis: function() {
        /// <summary>
        /// Get the name of the axis on which measures are displayed.
        /// </summary>
        /// <returns type="String">the axis name.</returns>

    },


    rows: function(val) {
        /// <summary>
        /// Get or sets the rows configuration.
        /// </summary>
        /// <param name="val" type="Array" >The rows configuration. Accepts the same values as the row option.</param>
        /// <returns type="Array">the current rows configuration.</returns>

    },


    schemaCatalogs: function() {
        /// <summary>
        /// Request catalogs information.
        /// </summary>
        /// <returns type="Object">Deferred object</returns>

    },


    schemaCubes: function() {
        /// <summary>
        /// Request cubes schema information.
        /// </summary>
        /// <returns type="Object">Deferred object</returns>

    },


    schemaDimensions: function() {
        /// <summary>
        /// Request dimensions schema information.
        /// </summary>
        /// <returns type="Object">Deferred object</returns>

    },


    schemaHierarchies: function(dimensionName) {
        /// <summary>
        /// Request hierarchies schema information.
        /// </summary>
        /// <param name="dimensionName" type="String" >The name of the dimensions which is 'owner' of the hierarchy.</param>
        /// <returns type="Object">Deferred object</returns>

    },


    schemaLevels: function(hierarchyName) {
        /// <summary>
        /// Request levels schema information.
        /// </summary>
        /// <param name="hierarchyName" type="String" >The name of the hierarchy which is 'owner' of the level.</param>
        /// <returns type="Object">Deferred object</returns>

    },


    schemaMeasures: function() {
        /// <summary>
        /// Request measures schema information.
        /// </summary>
        /// <returns type="Object">Deferred object</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppPivotDataSource = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.data.PivotDataSource widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.data.PivotDataSource">The DeskApp.data.PivotDataSource instance (if present).</returns>
};

$.fn.DeskAppPivotDataSource = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.data.PivotDataSource widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;columns — Array 
    /// &#10;The configuration of columns axis members. An array of JavaScript objects or strings. A JavaScript objects are interpreted as column descriptors. Strings are interpreted as the hierarchal name of the member.
    /// &#10;
    /// &#10;measures — Array|Object 
    /// &#10;The configuration of measures. An string array which values are interpreted as the name of the measures to be loaded.
/// &#10;Measures can be defined as a list of objects with name and type fields:
    /// &#10;
    /// &#10;rows — Array 
    /// &#10;The configuration of rows axis members. An array of JavaScript objects or strings. A JavaScript objects are interpreted as rows descriptors. Strings are interpreted as the hierarchal name of the member.
    /// &#10;
    /// &#10;transport — Object 
    /// &#10;The configuration used to load data items and discover schema information.
    /// &#10;
    /// &#10;schema — Object 
    /// &#10;The schema configuration of the PivotDataSource.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.data.SchedulerDataSource = function() { };

DeskApp.data.SchedulerDataSource.prototype = {




    expand: function(start,end) {
        /// <summary>
        /// Expands all recurring events in the data and returns a list of events for a specific period.
        /// </summary>
        /// <param name="start" type="Date" >The start date of the period.</param>
        /// <param name="end" type="Date" >The end date of the period.</param>
        /// <returns type="Array">the expanded list of scheduler events filtered by the specified start/end period.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppSchedulerDataSource = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.data.SchedulerDataSource widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.data.SchedulerDataSource">The DeskApp.data.SchedulerDataSource instance (if present).</returns>
};

$.fn.DeskAppSchedulerDataSource = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.data.SchedulerDataSource widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;schema — Object 
    /// &#10;The schema configuration of the SchedulerDataSource.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.data.SchedulerEvent = function() { };

DeskApp.data.SchedulerEvent.prototype = {




    clone: function(options,updateUid) {
        /// <summary>
        /// Clones the scheduler event.
        /// </summary>
        /// <param name="options" type="Object" >Additional options passed to the SchedulerEvent constructor.</param>
        /// <param name="updateUid" type="Boolean" >If you pass true the uid of the event will be updated.</param>
        /// <returns type="DeskApp.data.SchedulerEvent">the cloned scheduler event.</returns>

    },


    duration: function() {
        /// <summary>
        /// Returns the scheduler event length in milliseconds.
        /// </summary>
        /// <returns type="Number">the length of the event.</returns>

    },


    expand: function(start,end,timeZoneId) {
        /// <summary>
        /// Expands the event for a specific period based on the recurrenceRule option.
        /// </summary>
        /// <param name="start" type="Date" >The start date of the occurrence period.</param>
        /// <param name="end" type="Date" >The end date of the occurrence period.</param>
        /// <param name="timeZoneId" type="String" >The time zone ID used to convert the recurrence rule dates.</param>
        /// <returns type="Array">list of occurrences.</returns>

    },


    update: function(eventInfo) {
        /// <summary>
        /// Updates the scheduler event.
        /// </summary>
        /// <param name="eventInfo" type="Object" >The new values, which will be used to update the event.</param>

    },


    isMultiDay: function() {
        /// <summary>
        /// Checks whether the event is equal to or longer then twenty four hours.
        /// </summary>
        /// <returns type="Boolean">return true if event is equal to or longer then 24 hours.</returns>

    },


    isException: function() {
        /// <summary>
        /// Checks whether the event is a recurrence exception.
        /// </summary>
        /// <returns type="Boolean">return true if event is a recurrence exception.</returns>

    },


    isOccurrence: function() {
        /// <summary>
        /// Checks whether the event is an occurrence part of a recurring series.
        /// </summary>
        /// <returns type="Boolean">return true if event is an occurrence.</returns>

    },


    isRecurring: function() {
        /// <summary>
        /// Checks whether the event is part of a recurring series.
        /// </summary>
        /// <returns type="Boolean">return true if event is recurring.</returns>

    },


    isRecurrenceHead: function() {
        /// <summary>
        /// Checks whether the event is the head of a recurring series.
        /// </summary>
        /// <returns type="Boolean">return true if event is a recurrence head.</returns>

    },


    toOccurrence: function(options) {
        /// <summary>
        /// Converts the scheduler event to a event occurrence. Method will remove recurrenceRule, recurrenceException options, will add a recurrenceId field and will set id to the default one.
        /// </summary>
        /// <param name="options" type="Object" >Additional options passed to the SchedulerEvent constructor.</param>
        /// <returns type="DeskApp.data.SchedulerEvent">the occurrence.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppSchedulerEvent = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.data.SchedulerEvent widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.data.SchedulerEvent">The DeskApp.data.SchedulerEvent instance (if present).</returns>
};

$.fn.DeskAppSchedulerEvent = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.data.SchedulerEvent widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;description — String (default: "")
    /// &#10;The optional event description.
    /// &#10;
    /// &#10;end — Date 
    /// &#10;The date at which the scheduler event ends. The end date is mandatory.
    /// &#10;
    /// &#10;endTimezone — String (default: undefined)
    /// &#10;The timezone of the end date. If not specified the timezone will be used.The complete list of the supported timezones is available in the List of IANA time zones Wikipedia page.
    /// &#10;
    /// &#10;id — String|Number|Object 
    /// &#10;The mandatory unique identifier of the event.
    /// &#10;
    /// &#10;isAllDay — Boolean (default: false)
    /// &#10;If set to true the event is "all day". By default events are not all day.
    /// &#10;
    /// &#10;recurrenceException — String (default: undefined)
    /// &#10;The recurrence exceptions. A list of semi-colon separated dates formatted using the yyyyMMddTHHmmssZ format string.
    /// &#10;
    /// &#10;recurrenceId — String|Number|Object (default: undefined)
    /// &#10;The id of the recurrence parent event. Required for events that are recurrence exceptions.
    /// &#10;
    /// &#10;recurrenceRule — String (default: undefined)
    /// &#10;The recurrence rule describing the recurring pattern of the event. The format follows the iCal specification.
    /// &#10;
    /// &#10;start — Date 
    /// &#10;The date at which the scheduler event starts. The start date is mandatory.
    /// &#10;
    /// &#10;startTimezone — String (default: undefined)
    /// &#10;The timezone of the start date. If not specified the timezone will be used.The complete list of the supported timezones is available in the List of IANA time zones Wikipedia page.
    /// &#10;
    /// &#10;title — String (default: "")
    /// &#10;The title of the event which is displayed by the scheduler widget.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.data.TreeListDataSource = function() { };

DeskApp.data.TreeListDataSource.prototype = {




    load: function(model) {
        /// <summary>
        /// Loads the child nodes of a model.
        /// </summary>
        /// <param name="model" type="DeskApp.data.TreeListModel" >The model that must be loaded.</param>
        /// <returns type="Promise">A promise that will be resolved when the child nodes have been loaded, or rejected if an HTTP error occurs.</returns>

    },


    childNodes: function(model) {
        /// <summary>
        /// Child nodes for model.
        /// </summary>
        /// <param name="model" type="DeskApp.data.TreeListModel" >The model whose children must be returned.</param>
        /// <returns type="Array">of the child items.</returns>

    },


    rootNodes: function() {
        /// <summary>
        /// Return all root nodes.
        /// </summary>
        /// <returns type="Array">of the root items.</returns>

    },


    parentNode: function(model) {
        /// <summary>
        /// The parent of given node.
        /// </summary>
        /// <param name="model" type="DeskApp.data.TreeListModel" >The model whose parent must be returned.</param>
        /// <returns type="DeskApp.data.TreeListModel">parent of the node.</returns>

    },


    level: function(model) {
        /// <summary>
        /// The hierarchical level of the node.
        /// </summary>
        /// <param name="model" type="DeskApp.data.TreeListModel" >The model whose level must be calculated.</param>
        /// <returns type="Number">the hierachy level of the node.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppTreeListDataSource = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.data.TreeListDataSource widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.data.TreeListDataSource">The DeskApp.data.TreeListDataSource instance (if present).</returns>
};

$.fn.DeskAppTreeListDataSource = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.data.TreeListDataSource widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;schema — Object 
    /// &#10;The schema configuration of the TreeListDataSource.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.data.TreeListModel = function() { };

DeskApp.data.TreeListModel.prototype = {




    loaded: function() {
        /// <summary>
        /// Gets or sets the loaded flag of the TreeList. Setting the loaded flag to false allows reloading of child items.
        /// </summary>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppTreeListModel = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.data.TreeListModel widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.data.TreeListModel">The DeskApp.data.TreeListModel instance (if present).</returns>
};

$.fn.DeskAppTreeListModel = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.data.TreeListModel widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


if (!drawing) {
    drawing = {};
}

DeskApp.drawing.Arc = function() { };

DeskApp.drawing.Arc.prototype = {




    bbox: function() {
        /// <summary>
        /// Returns the bounding box of the element with transformations applied.
/// Inherited from Element.bbox
        /// </summary>
        /// <returns type="DeskApp.geometry.Rect">The bounding box of the element with transformations applied.</returns>

    },


    clip: function(clip) {
        /// <summary>
        /// Gets or sets the element clipping path.
/// Inherited from Element.clip
        /// </summary>
        /// <param name="clip" type="DeskApp.drawing.Path" >The element clipping path.</param>
        /// <returns type="DeskApp.drawing.Path">The current element clipping path.</returns>

    },


    clippedBBox: function() {
        /// <summary>
        /// Returns the bounding box of the element with clipping and transformations applied.
/// Inherited from Element.clippedBBox
        /// </summary>
        /// <returns type="DeskApp.geometry.Rect">The bounding box of the element with clipping transformations applied.</returns>

    },


    geometry: function(value) {
        /// <summary>
        /// Gets or sets the arc geometry.
        /// </summary>
        /// <param name="value" type="DeskApp.geometry.Arc" >The new geometry to use.</param>
        /// <returns type="DeskApp.geometry.Arc">The current arc geometry.</returns>

    },


    fill: function(color,opacity) {
        /// <summary>
        /// Sets the shape fill.
        /// </summary>
        /// <param name="color" type="String" >The fill color to set.</param>
        /// <param name="opacity" type="Number" >The fill opacity to set.</param>
        /// <returns type="DeskApp.drawing.Arc">The current instance to allow chaining.</returns>

    },


    opacity: function(opacity) {
        /// <summary>
        /// Gets or sets the element opacity.
/// Inherited from Element.opacityIf set, the stroke and fill opacity will be multiplied by the element opacity.
        /// </summary>
        /// <param name="opacity" type="Number" >The element opacity. Ranges from 0 (completely transparent) to 1 (completely opaque).</param>
        /// <returns type="Number">The current element opacity.</returns>

    },


    stroke: function(color,width,opacity) {
        /// <summary>
        /// Sets the shape stroke.
        /// </summary>
        /// <param name="color" type="String" >The stroke color to set.</param>
        /// <param name="width" type="Number" >The stroke width to set.</param>
        /// <param name="opacity" type="Number" >The stroke opacity to set.</param>
        /// <returns type="DeskApp.drawing.Arc">The current instance to allow chaining.</returns>

    },


    transform: function(transform) {
        /// <summary>
        /// Gets or sets the transformation of the element.
/// Inherited from Element.transform
        /// </summary>
        /// <param name="transform" type="DeskApp.geometry.Transformation" >The transformation to apply to the element.</param>
        /// <returns type="DeskApp.geometry.Transformation">The current transformation on the element.</returns>

    },


    visible: function(visible) {
        /// <summary>
        /// Gets or sets the visibility of the element.
/// Inherited from Element.visible
        /// </summary>
        /// <param name="visible" type="Boolean" >A flag indicating if the element should be visible.</param>
        /// <returns type="Boolean">true if the element is visible; false otherwise.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppArc = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.drawing.Arc widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.drawing.Arc">The DeskApp.drawing.Arc instance (if present).</returns>
};

$.fn.DeskAppArc = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.drawing.Arc widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;clip — DeskApp.drawing.Path 
    /// &#10;The element clipping path.
/// &#10;Inherited from Element.clip
    /// &#10;
    /// &#10;fill — DeskApp.drawing.FillOptions 
    /// &#10;The fill options of the shape.
    /// &#10;
    /// &#10;opacity — Number 
    /// &#10;The element opacity.
/// &#10;Inherited from Element.opacity
    /// &#10;
    /// &#10;stroke — DeskApp.drawing.StrokeOptions 
    /// &#10;The stroke options of the shape.
    /// &#10;
    /// &#10;transform — DeskApp.geometry.Transformation 
    /// &#10;The transformation to apply to this element.
/// &#10;Inherited from Element.transform
    /// &#10;
    /// &#10;visible — Boolean 
    /// &#10;A flag, indicating if the element is visible.
/// &#10;Inherited from Element.visible
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.drawing.Circle = function() { };

DeskApp.drawing.Circle.prototype = {




    bbox: function() {
        /// <summary>
        /// Returns the bounding box of the element with transformations applied.
/// Inherited from Element.bbox
        /// </summary>
        /// <returns type="DeskApp.geometry.Rect">The bounding box of the element with transformations applied.</returns>

    },


    clip: function(clip) {
        /// <summary>
        /// Gets or sets the element clipping path.
/// Inherited from Element.clip
        /// </summary>
        /// <param name="clip" type="DeskApp.drawing.Path" >The element clipping path.</param>
        /// <returns type="DeskApp.drawing.Path">The current element clipping path.</returns>

    },


    clippedBBox: function() {
        /// <summary>
        /// Returns the bounding box of the element with clipping and transformations applied.
/// Inherited from Element.clippedBBox
        /// </summary>
        /// <returns type="DeskApp.geometry.Rect">The bounding box of the element with clipping transformations applied.</returns>

    },


    geometry: function(value) {
        /// <summary>
        /// Gets or sets the circle geometry.
        /// </summary>
        /// <param name="value" type="DeskApp.geometry.Circle" >The new geometry to use.</param>
        /// <returns type="DeskApp.geometry.Circle">The current circle geometry.</returns>

    },


    fill: function(color,opacity) {
        /// <summary>
        /// Sets the shape fill.
        /// </summary>
        /// <param name="color" type="String" >The fill color to set.</param>
        /// <param name="opacity" type="Number" >The fill opacity to set.</param>
        /// <returns type="DeskApp.drawing.Circle">The current instance to allow chaining.</returns>

    },


    opacity: function(opacity) {
        /// <summary>
        /// Gets or sets the element opacity.
/// Inherited from Element.opacityIf set, the stroke and fill opacity will be multiplied by the element opacity.
        /// </summary>
        /// <param name="opacity" type="Number" >The element opacity. Ranges from 0 (completely transparent) to 1 (completely opaque).</param>
        /// <returns type="Number">The current element opacity.</returns>

    },


    stroke: function(color,width,opacity) {
        /// <summary>
        /// Sets the shape stroke.
        /// </summary>
        /// <param name="color" type="String" >The stroke color to set.</param>
        /// <param name="width" type="Number" >The stroke width to set.</param>
        /// <param name="opacity" type="Number" >The stroke opacity to set.</param>
        /// <returns type="DeskApp.drawing.Circle">The current instance to allow chaining.</returns>

    },


    transform: function(transform) {
        /// <summary>
        /// Gets or sets the transformation of the element.
/// Inherited from Element.transform
        /// </summary>
        /// <param name="transform" type="DeskApp.geometry.Transformation" >The transformation to apply to the element.</param>
        /// <returns type="DeskApp.geometry.Transformation">The current transformation on the element.</returns>

    },


    visible: function(visible) {
        /// <summary>
        /// Gets or sets the visibility of the element.
/// Inherited from Element.visible
        /// </summary>
        /// <param name="visible" type="Boolean" >A flag indicating if the element should be visible.</param>
        /// <returns type="Boolean">true if the element is visible; false otherwise.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppCircle = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.drawing.Circle widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.drawing.Circle">The DeskApp.drawing.Circle instance (if present).</returns>
};

$.fn.DeskAppCircle = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.drawing.Circle widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;clip — DeskApp.drawing.Path 
    /// &#10;The element clipping path.
/// &#10;Inherited from Element.clip
    /// &#10;
    /// &#10;fill — DeskApp.drawing.FillOptions 
    /// &#10;The fill options of the shape.
    /// &#10;
    /// &#10;opacity — Number 
    /// &#10;The element opacity.
/// &#10;Inherited from Element.opacity
    /// &#10;
    /// &#10;stroke — DeskApp.drawing.StrokeOptions 
    /// &#10;The stroke options of the shape.
    /// &#10;
    /// &#10;transform — DeskApp.geometry.Transformation 
    /// &#10;The transformation to apply to this element.
/// &#10;Inherited from Element.transform
    /// &#10;
    /// &#10;visible — Boolean 
    /// &#10;A flag, indicating if the element is visible.
/// &#10;Inherited from Element.visible
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.drawing.Element = function() { };

DeskApp.drawing.Element.prototype = {




    bbox: function() {
        /// <summary>
        /// Returns the bounding box of the element with transformations applied.
        /// </summary>
        /// <returns type="DeskApp.geometry.Rect">The bounding box of the element with transformations applied.</returns>

    },


    clip: function(clip) {
        /// <summary>
        /// Gets or sets the element clipping path.
        /// </summary>
        /// <param name="clip" type="DeskApp.drawing.Path" >The element clipping path.</param>
        /// <returns type="DeskApp.drawing.Path">The current element clipping path.</returns>

    },


    clippedBBox: function() {
        /// <summary>
        /// Returns the bounding box of the element with clipping and transformations applied.This is the rectangle that will fit around the actual rendered element.
        /// </summary>
        /// <returns type="DeskApp.geometry.Rect">The bounding box of the element with clipping and transformations applied.</returns>

    },


    opacity: function(opacity) {
        /// <summary>
        /// Gets or sets the element opacity.
        /// </summary>
        /// <param name="opacity" type="Number" >The element opacity. Ranges from 0 (completely transparent) to 1 (completely opaque).</param>
        /// <returns type="Number">The current element opacity.</returns>

    },


    transform: function(transform) {
        /// <summary>
        /// Gets or sets the transformation of the element.
        /// </summary>
        /// <param name="transform" type="DeskApp.geometry.Transformation" >The transformation to apply to the element.</param>
        /// <returns type="DeskApp.geometry.Transformation">The current transformation on the element.</returns>

    },


    visible: function(visible) {
        /// <summary>
        /// Gets or sets the visibility of the element.
        /// </summary>
        /// <param name="visible" type="Boolean" >A flag indicating if the element should be visible.</param>
        /// <returns type="Boolean">true if the element is visible; false otherwise.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppElement = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.drawing.Element widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.drawing.Element">The DeskApp.drawing.Element instance (if present).</returns>
};

$.fn.DeskAppElement = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.drawing.Element widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;clip — DeskApp.drawing.Path 
    /// &#10;The clipping path for this element.The path instance will be monitored for changes.
/// &#10;It can be replaced by calling the clip method.
    /// &#10;
    /// &#10;opacity — Number 
    /// &#10;The element opacity.
    /// &#10;
    /// &#10;transform — DeskApp.geometry.Transformation 
    /// &#10;The transformation to apply to this element.
    /// &#10;
    /// &#10;visible — Boolean 
    /// &#10;A flag, indicating if the element is visible.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.drawing.FillOptions = function() { };

DeskApp.drawing.FillOptions.prototype = {



    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppFillOptions = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.drawing.FillOptions widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.drawing.FillOptions">The DeskApp.drawing.FillOptions instance (if present).</returns>
};

$.fn.DeskAppFillOptions = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.drawing.FillOptions widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.drawing.Gradient = function() { };

DeskApp.drawing.Gradient.prototype = {




    addStop: function(offset,color,opacity) {
        /// <summary>
        /// Adds a color stop to the gradient.
        /// </summary>
        /// <param name="offset" type="Number" >The stop offset from the start of the element. Ranges from 0 (start of gradient) to 1 (end of gradient).</param>
        /// <param name="color" type="String" >The color in any of the following formats.| Format         | Description | ---            | --- | --- | red            | Basic or Extended CSS Color name | #ff0000        | Hex RGB value | rgb(255, 0, 0) | RGB valueSpecifying 'none', 'transparent' or '' (empty string) will clear the fill.</param>
        /// <param name="opacity" type="Number" >The fill opacity. Ranges from 0 (completely transparent) to 1 (completely opaque).</param>
        /// <returns type="DeskApp.drawing.GradientStop">The new gradient color stop.</returns>

    },


    removeStop: function(stop) {
        /// <summary>
        /// Removes a color stop from the gradient.
        /// </summary>
        /// <param name="stop" type="DeskApp.drawing.GradientStop" >The gradient color stop to remove.</param>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppGradient = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.drawing.Gradient widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.drawing.Gradient">The DeskApp.drawing.Gradient instance (if present).</returns>
};

$.fn.DeskAppGradient = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.drawing.Gradient widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;stops — Array 
    /// &#10;The color stops of the gradient.
/// &#10;Can contain either plain objects or GradientStop instances.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.drawing.GradientStop = function() { };

DeskApp.drawing.GradientStop.prototype = {



    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppGradientStop = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.drawing.GradientStop widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.drawing.GradientStop">The DeskApp.drawing.GradientStop instance (if present).</returns>
};

$.fn.DeskAppGradientStop = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.drawing.GradientStop widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;offset — Number 
    /// &#10;The stop offset from the start of the element.
/// &#10;Ranges from 0 (start of gradient) to 1 (end of gradient).
    /// &#10;
    /// &#10;color — String 
    /// &#10;The color in any of the following formats.| Format         | Description
/// &#10;| ---            | --- | ---
/// &#10;| red            | Basic or Extended CSS Color name
/// &#10;| #ff0000        | Hex RGB value
/// &#10;| rgb(255, 0, 0) | RGB valueSpecifying 'none', 'transparent' or '' (empty string) will clear the fill.
    /// &#10;
    /// &#10;opacity — Number 
    /// &#10;The fill opacity.
/// &#10;Ranges from 0 (completely transparent) to 1 (completely opaque).
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.drawing.Group = function() { };

DeskApp.drawing.Group.prototype = {




    append: function(element) {
        /// <summary>
        /// Appends the specified element as a last child of the group.
        /// </summary>
        /// <param name="element" type="DeskApp.drawing.Element" >The element to append. Multiple parameters are accepted.</param>

    },


    clear: function() {
        /// <summary>
        /// Removes all child elements from the group.
        /// </summary>

    },


    clip: function(clip) {
        /// <summary>
        /// Gets or sets the group clipping path.
/// Inherited from Element.clip
        /// </summary>
        /// <param name="clip" type="DeskApp.drawing.Path" >The group clipping path.</param>
        /// <returns type="DeskApp.drawing.Path">The current group clipping path.</returns>

    },


    clippedBBox: function() {
        /// <summary>
        /// Returns the bounding box of the element with clipping and transformations applied.
/// Inherited from Element.clippedBBox
        /// </summary>
        /// <returns type="DeskApp.geometry.Rect">The bounding box of the element with clipping transformations applied.</returns>

    },


    opacity: function(opacity) {
        /// <summary>
        /// Gets or sets the group opacity.
/// Inherited from Element.opacityThe opacity of any child groups and elements will be multiplied by this value.
        /// </summary>
        /// <param name="opacity" type="Number" >The group opacity. Ranges from 0 (completely transparent) to 1 (completely opaque).</param>
        /// <returns type="Number">The current group opacity.</returns>

    },


    remove: function(element) {
        /// <summary>
        /// Removes the specified element from the group.
        /// </summary>
        /// <param name="element" type="DeskApp.drawing.Element" >The element to remove.</param>

    },


    removeAt: function(index) {
        /// <summary>
        /// Removes the child element at the specified position.
        /// </summary>
        /// <param name="index" type="Number" >The index at which the element currently resides.</param>

    },


    visible: function(visible) {
        /// <summary>
        /// Gets or sets the visibility of the element.
        /// </summary>
        /// <param name="visible" type="Boolean" >A flag indicating if the element should be visible.</param>
        /// <returns type="Boolean">true if the element is visible; false otherwise.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppGroup = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.drawing.Group widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.drawing.Group">The DeskApp.drawing.Group instance (if present).</returns>
};

$.fn.DeskAppGroup = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.drawing.Group widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;clip — DeskApp.drawing.Path 
    /// &#10;The group clipping path.
/// &#10;Inherited from Element.clip
    /// &#10;
    /// &#10;opacity — Number 
    /// &#10;The group opacity.
/// &#10;Inherited from Element.opacityThe opacity of any child groups and elements will be multiplied by this value.
    /// &#10;
    /// &#10;pdf — DeskApp.drawing.PDFOptions 
    /// &#10;Page options to apply during PDF export.
    /// &#10;
    /// &#10;transform — DeskApp.geometry.Transformation 
    /// &#10;The transformation to apply to this group and its children.
/// &#10;Inherited from Element.transform
    /// &#10;
    /// &#10;visible — Boolean 
    /// &#10;A flag, indicating if the group and its children are visible.
/// &#10;Inherited from Element.visible
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.drawing.Image = function() { };

DeskApp.drawing.Image.prototype = {




    bbox: function() {
        /// <summary>
        /// Returns the bounding box of the element with transformations applied.
/// Inherited from Element.bbox
        /// </summary>
        /// <returns type="DeskApp.geometry.Rect">The bounding box of the element with transformations applied.</returns>

    },


    clip: function(clip) {
        /// <summary>
        /// Gets or sets the element clipping path.
/// Inherited from Element.clip
        /// </summary>
        /// <param name="clip" type="DeskApp.drawing.Path" >The element clipping path.</param>
        /// <returns type="DeskApp.drawing.Path">The current element clipping path.</returns>

    },


    clippedBBox: function() {
        /// <summary>
        /// Returns the bounding box of the element with clipping and transformations applied.
/// Inherited from Element.clippedBBox
        /// </summary>
        /// <returns type="DeskApp.geometry.Rect">The bounding box of the element with clipping transformations applied.</returns>

    },


    opacity: function(opacity) {
        /// <summary>
        /// Gets or sets the element opacity.
/// Inherited from Element.opacity
        /// </summary>
        /// <param name="opacity" type="Number" >The element opacity. Ranges from 0 (completely transparent) to 1 (completely opaque).</param>
        /// <returns type="Number">The current element opacity.</returns>

    },


    src: function(value) {
        /// <summary>
        /// Gets or sets the image source URL.
        /// </summary>
        /// <param name="value" type="String" >The new source URL.</param>
        /// <returns type="String">The current image source URL.</returns>

    },


    rect: function(value) {
        /// <summary>
        /// Gets or sets the rectangle defines the image position and size.
        /// </summary>
        /// <param name="value" type="DeskApp.geometry.Rect" >The new image rectangle.</param>
        /// <returns type="DeskApp.geometry.Rect">The current image rectangle.</returns>

    },


    transform: function(transform) {
        /// <summary>
        /// Gets or sets the transformation of the element.
/// Inherited from Element.transform
        /// </summary>
        /// <param name="transform" type="DeskApp.geometry.Transformation" >The transformation to apply to the element.</param>
        /// <returns type="DeskApp.geometry.Transformation">The current transformation on the element.</returns>

    },


    visible: function(visible) {
        /// <summary>
        /// Gets or sets the visibility of the element.
/// Inherited from Element.visible
        /// </summary>
        /// <param name="visible" type="Boolean" >A flag indicating if the element should be visible.</param>
        /// <returns type="Boolean">true if the element is visible; false otherwise.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppImage = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.drawing.Image widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.drawing.Image">The DeskApp.drawing.Image instance (if present).</returns>
};

$.fn.DeskAppImage = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.drawing.Image widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;clip — DeskApp.drawing.Path 
    /// &#10;The element clipping path.
/// &#10;Inherited from Element.clip
    /// &#10;
    /// &#10;opacity — Number 
    /// &#10;The element opacity.
/// &#10;Inherited from Element.opacity
    /// &#10;
    /// &#10;transform — DeskApp.geometry.Transformation 
    /// &#10;The transformation to apply to this element.
/// &#10;Inherited from Element.transform
    /// &#10;
    /// &#10;visible — Boolean 
    /// &#10;A flag, indicating if the element is visible.
/// &#10;Inherited from Element.visible
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.drawing.Layout = function() { };

DeskApp.drawing.Layout.prototype = {




    rect: function(rect) {
        /// <summary>
        /// Gets or sets the layout rectangle.
        /// </summary>
        /// <param name="rect" type="DeskApp.geometry.Rect" >The layout rectangle.</param>
        /// <returns type="DeskApp.geometry.Rect">The current rectangle.</returns>

    },


    reflow: function() {
        /// <summary>
        /// Arranges the elements based on the current options.
        /// </summary>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppLayout = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.drawing.Layout widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.drawing.Layout">The DeskApp.drawing.Layout instance (if present).</returns>
};

$.fn.DeskAppLayout = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.drawing.Layout widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;alignContent — String (default: "start")
    /// &#10;Specifies the alignment of the content.
    /// &#10;
    /// &#10;alignItems — String (default: "start")
    /// &#10;Specifies the alignment of the items based.
    /// &#10;
    /// &#10;justifyContent — String (default: "start")
    /// &#10;Specifies how should the content be justified.
    /// &#10;
    /// &#10;lineSpacing — Number (default: 0)
    /// &#10;Specifies the distance between the lines for wrapped layout.
    /// &#10;
    /// &#10;spacing — Number (default: 0)
    /// &#10;Specifies the distance between the elements.
    /// &#10;
    /// &#10;orientation — String (default: "horizontal")
    /// &#10;Specifies layout orientation. The supported values are:
    /// &#10;
    /// &#10;wrap — Boolean (default: true)
    /// &#10;Specifies the behavior when the elements size exceeds the rectangle size. If set to true, the elements will be moved to the next "line". If set to false, the layout will be scaled so that the elements fit in the rectangle.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.drawing.LinearGradient = function() { };

DeskApp.drawing.LinearGradient.prototype = {




    addStop: function(offset,color,opacity) {
        /// <summary>
        /// Adds a color stop to the gradient.
/// Inherited from Gradient.addStop
        /// </summary>
        /// <param name="offset" type="Number" ></param>
        /// <param name="color" type="String" >The color of the stop.</param>
        /// <param name="opacity" type="Number" >The fill opacity.</param>
        /// <returns type="DeskApp.drawing.GradientStop">The new gradient color stop.</returns>

    },


    end: function(end) {
        /// <summary>
        /// Gets or sets the end point of the gradient.
        /// </summary>
        /// <param name="end" type="Object" >The end point of the gradient.Coordinates are relative to the shape bounding box. For example [0, 0] is top left and [1, 1] is bottom right.</param>
        /// <returns type="DeskApp.geometry.Point">The current end point of the gradient.</returns>

    },


    start: function(start) {
        /// <summary>
        /// Gets or sets the start point of the gradient.
        /// </summary>
        /// <param name="start" type="Object" >The start point of the gradient.Coordinates are relative to the shape bounding box. For example [0, 0] is top left and [1, 1] is bottom right.</param>
        /// <returns type="DeskApp.geometry.Point">The current start point of the gradient.</returns>

    },


    removeStop: function(stop) {
        /// <summary>
        /// Removes a color stop from the gradient.
/// Inherited from Gradient.removeStop
        /// </summary>
        /// <param name="stop" type="DeskApp.drawing.GradientStop" >The gradient color stop to remove.</param>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppLinearGradient = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.drawing.LinearGradient widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.drawing.LinearGradient">The DeskApp.drawing.LinearGradient instance (if present).</returns>
};

$.fn.DeskAppLinearGradient = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.drawing.LinearGradient widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;stops — Array 
    /// &#10;The color stops of the gradient.
/// &#10;Can contain either plain objects or GradientStop instances.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.drawing.MultiPath = function() { };

DeskApp.drawing.MultiPath.prototype = {




    bbox: function() {
        /// <summary>
        /// Returns the bounding box of the element with transformations applied.
/// Inherited from Element.bbox
        /// </summary>
        /// <returns type="DeskApp.geometry.Rect">The bounding box of the element with transformations applied.</returns>

    },


    clip: function(clip) {
        /// <summary>
        /// Gets or sets the element clipping path.
/// Inherited from Element.clip
        /// </summary>
        /// <param name="clip" type="DeskApp.drawing.Path" >The element clipping path.</param>
        /// <returns type="DeskApp.drawing.Path">The current element clipping path.</returns>

    },


    clippedBBox: function() {
        /// <summary>
        /// Returns the bounding box of the element with clipping and transformations applied.
/// Inherited from Element.clippedBBox
        /// </summary>
        /// <returns type="DeskApp.geometry.Rect">The bounding box of the element with clipping transformations applied.</returns>

    },


    close: function() {
        /// <summary>
        /// Closes the current sub-path by linking its current end point with its start point.
        /// </summary>
        /// <returns type="DeskApp.drawing.MultiPath">The current instance to allow chaining.</returns>

    },


    curveTo: function(controlOut,controlIn) {
        /// <summary>
        /// Draws a cubic Bézier curve (with two control points).A quadratic Bézier curve (with one control point) can be plotted by making the control point equal.
        /// </summary>
        /// <param name="controlOut" type="Object" >The first control point for the curve.</param>
        /// <param name="controlIn" type="Object" >The second control point for the curve.</param>
        /// <returns type="DeskApp.drawing.MultiPath">The current instance to allow chaining.</returns>

    },


    fill: function(color,opacity) {
        /// <summary>
        /// Sets the shape fill.
        /// </summary>
        /// <param name="color" type="String" >The fill color to set.</param>
        /// <param name="opacity" type="Number" >The fill opacity to set.</param>
        /// <returns type="DeskApp.drawing.MultiPath">The current instance to allow chaining.</returns>

    },


    lineTo: function(x,y) {
        /// <summary>
        /// Draws a straight line to the specified absolute coordinates.
        /// </summary>
        /// <param name="x" type="Object" >The line end X coordinate or a Point/Array with X and Y coordinates.</param>
        /// <param name="y" type="Number" >The line end Y coordinate.Optional if the first parameter is a Point/Array.</param>
        /// <returns type="DeskApp.drawing.MultiPath">The current instance to allow chaining.</returns>

    },


    moveTo: function(x,y) {
        /// <summary>
        /// Creates a new sub-path or clears all segments and moves the starting point to the specified absolute coordinates.
        /// </summary>
        /// <param name="x" type="Object" >The starting X coordinate or a Point/Array with X and Y coordinates.</param>
        /// <param name="y" type="Number" >The starting Y coordinate.Optional if the first parameter is a Point/Array.</param>
        /// <returns type="DeskApp.drawing.MultiPath">The current instance to allow chaining.</returns>

    },


    opacity: function(opacity) {
        /// <summary>
        /// Gets or sets the element opacity.
/// Inherited from Element.opacityIf set, the stroke and fill opacity will be multiplied by the element opacity.
        /// </summary>
        /// <param name="opacity" type="Number" >The element opacity. Ranges from 0 (completely transparent) to 1 (completely opaque).</param>
        /// <returns type="Number">The current element opacity.</returns>

    },


    stroke: function(color,width,opacity) {
        /// <summary>
        /// Sets the shape stroke.
        /// </summary>
        /// <param name="color" type="String" >The stroke color to set.</param>
        /// <param name="width" type="Number" >The stroke width to set.</param>
        /// <param name="opacity" type="Number" >The stroke opacity to set.</param>
        /// <returns type="DeskApp.drawing.MultiPath">The current instance to allow chaining.</returns>

    },


    transform: function(transform) {
        /// <summary>
        /// Gets or sets the transformation of the element.
/// Inherited from Element.transform
        /// </summary>
        /// <param name="transform" type="DeskApp.geometry.Transformation" >The transformation to apply to the element.</param>
        /// <returns type="DeskApp.geometry.Transformation">The current transformation on the element.</returns>

    },


    visible: function(visible) {
        /// <summary>
        /// Gets or sets the visibility of the element.
/// Inherited from Element.visible
        /// </summary>
        /// <param name="visible" type="Boolean" >A flag indicating if the element should be visible.</param>
        /// <returns type="Boolean">true if the element is visible; false otherwise.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMultiPath = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.drawing.MultiPath widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.drawing.MultiPath">The DeskApp.drawing.MultiPath instance (if present).</returns>
};

$.fn.DeskAppMultiPath = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.drawing.MultiPath widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;clip — DeskApp.drawing.Path 
    /// &#10;The element clipping path.
/// &#10;Inherited from Element.clip
    /// &#10;
    /// &#10;fill — DeskApp.drawing.FillOptions 
    /// &#10;The fill options of the shape.
    /// &#10;
    /// &#10;opacity — Number 
    /// &#10;The element opacity.
/// &#10;Inherited from Element.opacity
    /// &#10;
    /// &#10;stroke — DeskApp.drawing.StrokeOptions 
    /// &#10;The stroke options of the shape.
    /// &#10;
    /// &#10;transform — DeskApp.geometry.Transformation 
    /// &#10;The transformation to apply to this element.
/// &#10;Inherited from Element.transform
    /// &#10;
    /// &#10;visible — Boolean 
    /// &#10;A flag, indicating if the element is visible.
/// &#10;Inherited from Element.visible
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.drawing.OptionsStore = function() { };

DeskApp.drawing.OptionsStore.prototype = {




    get: function(field) {
        /// <summary>
        /// Gets the value of the specified option.
        /// </summary>
        /// <param name="field" type="String" >The field name to retrieve. Must be a fully qualified name (e.g. "foo.bar") for nested options.</param>
        /// <returns type="Object">The current option value.</returns>

    },


    set: function(field,value) {
        /// <summary>
        /// Sets the value of the specified option.
        /// </summary>
        /// <param name="field" type="String" >The name of the option to set. Must be a fully qualified name (e.g. "foo.bar") for nested options.</param>
        /// <param name="value" type="Object" >The new option value.If the new value is exactly the same as the new value the operation will not trigger options change on the observer (if any).</param>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppOptionsStore = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.drawing.OptionsStore widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.drawing.OptionsStore">The DeskApp.drawing.OptionsStore instance (if present).</returns>
};

$.fn.DeskAppOptionsStore = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.drawing.OptionsStore widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.drawing.PDFOptions = function() { };

DeskApp.drawing.PDFOptions.prototype = {



    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppPDFOptions = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.drawing.PDFOptions widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.drawing.PDFOptions">The DeskApp.drawing.PDFOptions instance (if present).</returns>
};

$.fn.DeskAppPDFOptions = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.drawing.PDFOptions widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.drawing.Path = function() { };

DeskApp.drawing.Path.prototype = {




    bbox: function() {
        /// <summary>
        /// Returns the bounding box of the element with transformations applied.
/// Inherited from Element.bbox
        /// </summary>
        /// <returns type="DeskApp.geometry.Rect">The bounding box of the element with transformations applied.</returns>

    },


    clip: function(clip) {
        /// <summary>
        /// Gets or sets the element clipping path.
/// Inherited from Element.clip
        /// </summary>
        /// <param name="clip" type="DeskApp.drawing.Path" >The element clipping path.</param>
        /// <returns type="DeskApp.drawing.Path">The current element clipping path.</returns>

    },


    clippedBBox: function() {
        /// <summary>
        /// Returns the bounding box of the element with clipping and transformations applied.
/// Inherited from Element.clippedBBox
        /// </summary>
        /// <returns type="DeskApp.geometry.Rect">The bounding box of the element with clipping transformations applied.</returns>

    },


    close: function() {
        /// <summary>
        /// Closes the path by linking the current end point with the start point.
        /// </summary>
        /// <returns type="DeskApp.drawing.Path">The current instance to allow chaining.</returns>

    },


    curveTo: function(controlOut,controlIn) {
        /// <summary>
        /// Draws a cubic Bézier curve (with two control points).A quadratic Bézier curve (with one control point) can be plotted by making the control point equal.
        /// </summary>
        /// <param name="controlOut" type="Object" >The first control point for the curve.</param>
        /// <param name="controlIn" type="Object" >The second control point for the curve.</param>
        /// <returns type="DeskApp.drawing.Path">The current instance to allow chaining.</returns>

    },


    fill: function(color,opacity) {
        /// <summary>
        /// Sets the shape fill.
        /// </summary>
        /// <param name="color" type="String" >The fill color to set.</param>
        /// <param name="opacity" type="Number" >The fill opacity to set.</param>
        /// <returns type="DeskApp.drawing.Path">The current instance to allow chaining.</returns>

    },


    lineTo: function(x,y) {
        /// <summary>
        /// Draws a straight line to the specified absolute coordinates.
        /// </summary>
        /// <param name="x" type="Object" >The line end X coordinate or a Point/Array with X and Y coordinates.</param>
        /// <param name="y" type="Number" >The line end Y coordinate.Optional if the first parameter is a Point/Array.</param>
        /// <returns type="DeskApp.drawing.Path">The current instance to allow chaining.</returns>

    },


    moveTo: function(x,y) {
        /// <summary>
        /// Clears all existing segments and moves the starting point to the specified absolute coordinates.
        /// </summary>
        /// <param name="x" type="Object" >The starting X coordinate or a Point/Array with X and Y coordinates.</param>
        /// <param name="y" type="Number" >The starting Y coordinate.Optional if the first parameter is a Point/Array.</param>
        /// <returns type="DeskApp.drawing.Path">The current instance to allow chaining.</returns>

    },


    opacity: function(opacity) {
        /// <summary>
        /// Gets or sets the element opacity.
/// Inherited from Element.opacityIf set, the stroke and fill opacity will be multiplied by the element opacity.
        /// </summary>
        /// <param name="opacity" type="Number" >The element opacity. Ranges from 0 (completely transparent) to 1 (completely opaque).</param>
        /// <returns type="Number">The current element opacity.</returns>

    },


    stroke: function(color,width,opacity) {
        /// <summary>
        /// Sets the shape stroke.
        /// </summary>
        /// <param name="color" type="String" >The stroke color to set.</param>
        /// <param name="width" type="Number" >The stroke width to set.</param>
        /// <param name="opacity" type="Number" >The stroke opacity to set.</param>
        /// <returns type="DeskApp.drawing.Path">The current instance to allow chaining.</returns>

    },


    transform: function(transform) {
        /// <summary>
        /// Gets or sets the transformation of the element.
/// Inherited from Element.transform
        /// </summary>
        /// <param name="transform" type="DeskApp.geometry.Transformation" >The transformation to apply to the element.</param>
        /// <returns type="DeskApp.geometry.Transformation">The current transformation on the element.</returns>

    },


    visible: function(visible) {
        /// <summary>
        /// Gets or sets the visibility of the element.
/// Inherited from Element.visible
        /// </summary>
        /// <param name="visible" type="Boolean" >A flag indicating if the element should be visible.</param>
        /// <returns type="Boolean">true if the element is visible; false otherwise.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppPath = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.drawing.Path widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.drawing.Path">The DeskApp.drawing.Path instance (if present).</returns>
};

$.fn.DeskAppPath = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.drawing.Path widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;clip — DeskApp.drawing.Path 
    /// &#10;The element clipping path.
/// &#10;Inherited from Element.clip
    /// &#10;
    /// &#10;fill — DeskApp.drawing.FillOptions 
    /// &#10;The fill options of the shape.
    /// &#10;
    /// &#10;opacity — Number 
    /// &#10;The element opacity.
/// &#10;Inherited from Element.opacity
    /// &#10;
    /// &#10;stroke — DeskApp.drawing.StrokeOptions 
    /// &#10;The stroke options of the shape.
    /// &#10;
    /// &#10;transform — DeskApp.geometry.Transformation 
    /// &#10;The transformation to apply to this element.
/// &#10;Inherited from Element.transform
    /// &#10;
    /// &#10;visible — Boolean 
    /// &#10;A flag, indicating if the element is visible.
/// &#10;Inherited from Element.visible
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.drawing.RadialGradient = function() { };

DeskApp.drawing.RadialGradient.prototype = {




    addStop: function(offset,color,opacity) {
        /// <summary>
        /// Adds a color stop to the gradient.
/// Inherited from Gradient.addStop
        /// </summary>
        /// <param name="offset" type="Number" ></param>
        /// <param name="color" type="String" >The color of the stop.</param>
        /// <param name="opacity" type="Number" >The fill opacity.</param>
        /// <returns type="DeskApp.drawing.GradientStop">The new gradient color stop.</returns>

    },


    center: function(center) {
        /// <summary>
        /// Gets or sets the center point of the gradient.
        /// </summary>
        /// <param name="center" type="Object" >The center point of the gradient.Coordinates are relative to the shape bounding box. For example [0, 0] is top left and [1, 1] is bottom right.</param>
        /// <returns type="DeskApp.geometry.Point">The current radius of the gradient.</returns>

    },


    radius: function(value) {
        /// <summary>
        /// Gets or sets the radius of the gradient.
        /// </summary>
        /// <param name="value" type="Number" >The new radius of the gradient.</param>
        /// <returns type="Number">The current radius of the gradient.</returns>

    },


    removeStop: function(stop) {
        /// <summary>
        /// Removes a color stop from the gradient.
/// Inherited from Gradient.removeStop
        /// </summary>
        /// <param name="stop" type="DeskApp.drawing.GradientStop" >The gradient color stop to remove.</param>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppRadialGradient = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.drawing.RadialGradient widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.drawing.RadialGradient">The DeskApp.drawing.RadialGradient instance (if present).</returns>
};

$.fn.DeskAppRadialGradient = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.drawing.RadialGradient widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;center — Array|DeskApp.geometry.Point 
    /// &#10;The center of the gradient.Coordinates are relative to the shape bounding box.
/// &#10;For example [0, 0] is top left and [1, 1] is bottom right.
    /// &#10;
    /// &#10;radius — Number (default: 1)
    /// &#10;The radius of the radial gradient relative to the shape bounding box.
    /// &#10;
    /// &#10;stops — Array 
    /// &#10;The color stops of the gradient.
/// &#10;Can contain either plain objects or GradientStop instances.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.drawing.Segment = function() { };

DeskApp.drawing.Segment.prototype = {




    anchor: function(value) {
        /// <summary>
        /// Gets or sets the segment anchor point.The setter returns the current Segment to allow chaining.
        /// </summary>
        /// <param name="value" type="DeskApp.geometry.Point" >The new anchor point.</param>
        /// <returns type="DeskApp.geometry.Point">The current anchor point.</returns>

    },


    controlIn: function(value) {
        /// <summary>
        /// Gets or sets the first curve control point of this segment.The setter returns the current Segment to allow chaining.
        /// </summary>
        /// <param name="value" type="DeskApp.geometry.Point" >The new control point.</param>
        /// <returns type="DeskApp.geometry.Point">The current control point.</returns>

    },


    controlOut: function(value) {
        /// <summary>
        /// Gets or sets the second curve control point of this segment.The setter returns the current Segment to allow chaining.
        /// </summary>
        /// <param name="value" type="DeskApp.geometry.Point" >The new control point.</param>
        /// <returns type="DeskApp.geometry.Point">The current control point.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppSegment = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.drawing.Segment widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.drawing.Segment">The DeskApp.drawing.Segment instance (if present).</returns>
};

$.fn.DeskAppSegment = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.drawing.Segment widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.drawing.StrokeOptions = function() { };

DeskApp.drawing.StrokeOptions.prototype = {



    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppStrokeOptions = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.drawing.StrokeOptions widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.drawing.StrokeOptions">The DeskApp.drawing.StrokeOptions instance (if present).</returns>
};

$.fn.DeskAppStrokeOptions = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.drawing.StrokeOptions widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.drawing.Surface = function() { };

DeskApp.drawing.Surface.prototype = {




    clear: function() {
        /// <summary>
        /// Clears the drawing surface.
        /// </summary>

    },


    draw: function(element) {
        /// <summary>
        /// Draws the element and its children on the surface.
/// Existing elements will remain visible.
        /// </summary>
        /// <param name="element" type="DeskApp.drawing.Element" >The element to draw.</param>

    },


    eventTarget: function(e) {
        /// <summary>
        /// Returns the target drawing element of a DOM event.
        /// </summary>
        /// <param name="e" type="Object" >The original DOM or jQuery event object.</param>
        /// <returns type="DeskApp.drawing.Element">The target drawing element, if any.</returns>

    },


    resize: function(force) {
        /// <summary>
        /// Resizes the surface to match the size of the container.
        /// </summary>
        /// <param name="force" type="Boolean" >Whether to proceed with resizing even if the container dimensions have not changed.</param>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppSurface = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.drawing.Surface widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.drawing.Surface">The DeskApp.drawing.Surface instance (if present).</returns>
};

$.fn.DeskAppSurface = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.drawing.Surface widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;type — String 
    /// &#10;The preferred type of surface to create.
/// &#10;Supported types (case insensitive):
/// &#10;- svg
/// &#10;- canvas
/// &#10;- vmlThis option will be ignored if not supported by the browser.
/// &#10;See Supported Browsers
    /// &#10;
    /// &#10;height — String (default: "100%")
    /// &#10;The height of the surface element.
/// &#10;By default the surface will expand to fill the height of the first positioned container.
    /// &#10;
    /// &#10;width — String (default: "100%")
    /// &#10;The width of the surface element.
/// &#10;By default the surface will expand to fill the width of the first positioned container.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.drawing.Text = function() { };

DeskApp.drawing.Text.prototype = {




    bbox: function() {
        /// <summary>
        /// Returns the bounding box of the element with transformations applied.
/// Inherited from Element.bbox
        /// </summary>
        /// <returns type="DeskApp.geometry.Rect">The bounding box of the element with transformations applied.</returns>

    },


    clip: function(clip) {
        /// <summary>
        /// Gets or sets the element clipping path.
/// Inherited from Element.clip
        /// </summary>
        /// <param name="clip" type="DeskApp.drawing.Path" >The element clipping path.</param>
        /// <returns type="DeskApp.drawing.Path">The current element clipping path.</returns>

    },


    clippedBBox: function() {
        /// <summary>
        /// Returns the bounding box of the element with clipping and transformations applied.
/// Inherited from Element.clippedBBox
        /// </summary>
        /// <returns type="DeskApp.geometry.Rect">The bounding box of the element with clipping transformations applied.</returns>

    },


    content: function(value) {
        /// <summary>
        /// Gets or sets the text content.
        /// </summary>
        /// <param name="value" type="String" >The new text content to set.</param>
        /// <returns type="String">The current content of the text.</returns>

    },


    fill: function(color,opacity) {
        /// <summary>
        /// Sets the text fill.
        /// </summary>
        /// <param name="color" type="String" >The fill color to set.</param>
        /// <param name="opacity" type="Number" >The fill opacity to set.</param>
        /// <returns type="DeskApp.drawing.Text">The current instance to allow chaining.</returns>

    },


    opacity: function(opacity) {
        /// <summary>
        /// Gets or sets the element opacity.
/// Inherited from Element.opacityIf set, the stroke and fill opacity will be multiplied by the element opacity.
        /// </summary>
        /// <param name="opacity" type="Number" >The element opacity. Ranges from 0 (completely transparent) to 1 (completely opaque).</param>
        /// <returns type="Number">The current element opacity.</returns>

    },


    position: function(value) {
        /// <summary>
        /// Gets or sets the position of the text upper left corner.
        /// </summary>
        /// <param name="value" type="DeskApp.geometry.Point" >The new position of the text upper left corner.</param>
        /// <returns type="DeskApp.geometry.Point">The current position of the text upper left corner.</returns>

    },


    stroke: function(color,width,opacity) {
        /// <summary>
        /// Sets the text stroke.
        /// </summary>
        /// <param name="color" type="String" >The stroke color to set.</param>
        /// <param name="width" type="Number" >The stroke width to set.</param>
        /// <param name="opacity" type="Number" >The stroke opacity to set.</param>
        /// <returns type="DeskApp.drawing.Text">The current instance to allow chaining.</returns>

    },


    transform: function(transform) {
        /// <summary>
        /// Gets or sets the transformation of the element.
/// Inherited from Element.transform
        /// </summary>
        /// <param name="transform" type="DeskApp.geometry.Transformation" >The transformation to apply to the element.</param>
        /// <returns type="DeskApp.geometry.Transformation">The current transformation on the element.</returns>

    },


    visible: function(visible) {
        /// <summary>
        /// Gets or sets the visibility of the element.
/// Inherited from Element.visible
        /// </summary>
        /// <param name="visible" type="Boolean" >A flag indicating if the element should be visible.</param>
        /// <returns type="Boolean">true if the element is visible; false otherwise.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppText = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.drawing.Text widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.drawing.Text">The DeskApp.drawing.Text instance (if present).</returns>
};

$.fn.DeskAppText = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.drawing.Text widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;clip — DeskApp.drawing.Path 
    /// &#10;The element clipping path.
/// &#10;Inherited from Element.clip
    /// &#10;
    /// &#10;fill — DeskApp.drawing.FillOptions 
    /// &#10;The fill options of the text.
    /// &#10;
    /// &#10;font — String 
    /// &#10;The font to use for rendering the text.
/// &#10;Accepts the standard CSS font syntax.Examples of valid font values:
/// &#10;* Size and family: "2em 'Open Sans', sans-serif"
/// &#10;* Style, size and family: "italic 2em 'Open Sans', sans-serif"
    /// &#10;
    /// &#10;opacity — Number 
    /// &#10;The element opacity.
/// &#10;Inherited from Element.opacity
    /// &#10;
    /// &#10;stroke — DeskApp.drawing.StrokeOptions 
    /// &#10;The stroke options of the text.
    /// &#10;
    /// &#10;transform — DeskApp.geometry.Transformation 
    /// &#10;The transformation to apply to this element.
/// &#10;Inherited from Element.transform
    /// &#10;
    /// &#10;visible — Boolean 
    /// &#10;A flag, indicating if the element is visible.
/// &#10;Inherited from Element.visible
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


if (!effects) {
    effects = {};
}

DeskApp.geometry.Arc = function() { };

DeskApp.geometry.Arc.prototype = {




    bbox: function(matrix) {
        /// <summary>
        /// Returns the bounding box of this arc after applying the specified transformation matrix.
        /// </summary>
        /// <param name="matrix" type="DeskApp.geometry.Matrix" >Transformation matrix to apply.</param>
        /// <returns type="DeskApp.geometry.Rect">The bounding box after applying the transformation matrix.</returns>

    },


    getAnticlockwise: function() {
        /// <summary>
        /// Gets the arc anticlokwise flag.
        /// </summary>
        /// <returns type="Boolean">The anticlokwise flag of the arc.</returns>

    },


    getCenter: function() {
        /// <summary>
        /// Gets the arc center location.
        /// </summary>
        /// <returns type="DeskApp.geometry.Point">The location of the arc center.</returns>

    },


    getEndAngle: function() {
        /// <summary>
        /// Gets the end angle of the arc in decimal degrees.
/// Measured in clockwise direction with 0 pointing "right".
        /// </summary>
        /// <returns type="Number">The end angle of the arc.</returns>

    },


    getRadiusX: function() {
        /// <summary>
        /// Gets the x radius of the arc.
        /// </summary>
        /// <returns type="Number">The x radius of the arc.</returns>

    },


    getRadiusY: function() {
        /// <summary>
        /// Gets the y radius of the arc.
        /// </summary>
        /// <returns type="Number">The y radius of the arc.</returns>

    },


    getStartAngle: function() {
        /// <summary>
        /// Gets the start angle of the arc in decimal degrees.
/// Measured in clockwise direction with 0 pointing "right".
        /// </summary>
        /// <returns type="Number">The start angle of the arc.</returns>

    },


    pointAt: function(angle) {
        /// <summary>
        /// Gets the location of a point on the arc's circumference at a given angle.
        /// </summary>
        /// <param name="angle" type="Number" >Angle in decimal degrees. Measured in clockwise direction with 0 pointing "right". Negative values or values greater than 360 will be normalized.</param>
        /// <returns type="DeskApp.geometry.Point">The point on the arc's circumference.</returns>

    },


    setAnticlockwise: function(value) {
        /// <summary>
        /// Sets the arc anticlokwise flag.
        /// </summary>
        /// <param name="value" type="Boolean" >The new anticlockwise value.</param>
        /// <returns type="DeskApp.geometry.Arc">The current arc instance.</returns>

    },


    setCenter: function(value) {
        /// <summary>
        /// Sets the arc center location.
        /// </summary>
        /// <param name="value" type="DeskApp.geometry.Point" >The new arc center.</param>
        /// <returns type="DeskApp.geometry.Arc">The current arc instance.</returns>

    },


    setEndAngle: function(value) {
        /// <summary>
        /// Sets the end angle of the arc in decimal degrees.
/// Measured in clockwise direction with 0 pointing "right".
        /// </summary>
        /// <param name="value" type="Number" >The new arc end angle.</param>
        /// <returns type="DeskApp.geometry.Arc">The current arc instance.</returns>

    },


    setRadiusX: function(value) {
        /// <summary>
        /// Sets the x radius of the arc.
        /// </summary>
        /// <param name="value" type="Number" >The new arc x radius.</param>
        /// <returns type="DeskApp.geometry.Arc">The current arc instance.</returns>

    },


    setRadiusY: function(value) {
        /// <summary>
        /// Sets the y radius of the arc.
        /// </summary>
        /// <param name="value" type="Number" >The new arc y radius.</param>
        /// <returns type="DeskApp.geometry.Arc">The current arc instance.</returns>

    },


    setStartAngle: function(value) {
        /// <summary>
        /// Sets the start angle of the arc in decimal degrees.
/// Measured in clockwise direction with 0 pointing "right".
        /// </summary>
        /// <param name="value" type="Number" >The new arc atart angle.</param>
        /// <returns type="DeskApp.geometry.Arc">The current arc instance.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppArc = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.geometry.Arc widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.geometry.Arc">The DeskApp.geometry.Arc instance (if present).</returns>
};

$.fn.DeskAppArc = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.geometry.Arc widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.geometry.Circle = function() { };

DeskApp.geometry.Circle.prototype = {




    bbox: function(matrix) {
        /// <summary>
        /// Returns the bounding box of this circle after applying the
/// specified transformation matrix.
        /// </summary>
        /// <param name="matrix" type="DeskApp.geometry.Matrix" >Transformation matrix to apply.</param>
        /// <returns type="DeskApp.geometry.Rect">The bounding box after applying the transformation matrix.</returns>

    },


    clone: function() {
        /// <summary>
        /// Creates a new instance with the same center and radius.
        /// </summary>
        /// <returns type="DeskApp.geometry.Circle">A new Circle instance with the same center and radius.</returns>

    },


    equals: function(other) {
        /// <summary>
        /// Compares this circle with another instance.
        /// </summary>
        /// <param name="other" type="DeskApp.geometry.Circle" >The circle to compare with.</param>
        /// <returns type="Boolean">true if the point coordinates match; false otherwise.</returns>

    },


    getCenter: function() {
        /// <summary>
        /// Gets the circle center location.
        /// </summary>
        /// <returns type="DeskApp.geometry.Point">The location of the circle center.</returns>

    },


    getRadius: function() {
        /// <summary>
        /// Gets the circle radius.
        /// </summary>
        /// <returns type="Number">The radius of the circle.</returns>

    },


    pointAt: function(angle) {
        /// <summary>
        /// Gets the location of a point on the circle's circumference at a given angle.
        /// </summary>
        /// <param name="angle" type="Number" >Angle in decimal degrees. Measured in clockwise direction with 0 pointing "right". Negative values or values greater than 360 will be normalized.</param>
        /// <returns type="DeskApp.geometry.Point">The point on the circle's circumference.</returns>

    },


    setCenter: function(value) {
        /// <summary>
        /// Sets the location of the circle center.
        /// </summary>
        /// <param name="value" type="Object" >The new center Point or equivalent [x, y] array.</param>
        /// <returns type="DeskApp.geometry.Point">The location of the circle center.</returns>

    },


    setRadius: function(value) {
        /// <summary>
        /// Sets the circle radius.
        /// </summary>
        /// <param name="value" type="Number" >The new circle radius.</param>
        /// <returns type="DeskApp.geometry.Circle">The current circle instance.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppCircle = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.geometry.Circle widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.geometry.Circle">The DeskApp.geometry.Circle instance (if present).</returns>
};

$.fn.DeskAppCircle = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.geometry.Circle widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.geometry.Matrix = function() { };

DeskApp.geometry.Matrix.prototype = {




    clone: function() {
        /// <summary>
        /// Creates a new instance with the same element values.
        /// </summary>
        /// <returns type="DeskApp.geometry.Matrix">A new Matrix instance with the same element values.</returns>

    },


    equals: function(other) {
        /// <summary>
        /// Compares this matrix with another instance.
        /// </summary>
        /// <param name="other" type="DeskApp.geometry.Matrix" >The matrix instance to compare with.</param>
        /// <returns type="Boolean">true if the matrix elements match; false otherwise.</returns>

    },


    round: function(digits) {
        /// <summary>
        /// Rounds the matrix elements to the specified number of fractional digits.
        /// </summary>
        /// <param name="digits" type="Number" >Number of fractional digits.</param>
        /// <returns type="DeskApp.geometry.Matrix">The current matrix instance.</returns>

    },


    multiplyCopy: function(matrix) {
        /// <summary>
        /// Multiplies the matrix with another one and returns the result as new instance.
/// The current instance elements are not altered.
        /// </summary>
        /// <param name="matrix" type="DeskApp.geometry.Matrix" >The matrix to multiply by.</param>
        /// <returns type="DeskApp.geometry.Matrix">The result of the multiplication.</returns>

    },


    toArray: function(digits) {
        /// <summary>
        /// Returns the matrix elements as an [a, b, c, d, e, f] array.
        /// </summary>
        /// <param name="digits" type="Number" >(Optional) Number of fractional digits.</param>
        /// <returns type="Array">An array representation of the matrix.</returns>

    },


    toString: function(digits,separator) {
        /// <summary>
        /// Formats the matrix elements as a string.
        /// </summary>
        /// <param name="digits" type="Number" >(Optional) Number of fractional digits.</param>
        /// <param name="separator" type="String" >The separator to place between elements.</param>
        /// <returns type="String">A string representation of the matrix, e.g. "1, 0, 0, 1, 0, 0".</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMatrix = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.geometry.Matrix widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.geometry.Matrix">The DeskApp.geometry.Matrix instance (if present).</returns>
};

$.fn.DeskAppMatrix = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.geometry.Matrix widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.geometry.Point = function() { };

DeskApp.geometry.Point.prototype = {




    clone: function() {
        /// <summary>
        /// Creates a new instance with the same coordinates.
        /// </summary>
        /// <returns type="DeskApp.geometry.Point">A new Point instance with the same coordinates.</returns>

    },


    distanceTo: function(point) {
        /// <summary>
        /// Calculates the distance to another point.
        /// </summary>
        /// <param name="point" type="DeskApp.geometry.Point" >The point to calculate the distance to.</param>
        /// <returns type="Number">The straight line distance to the given point.</returns>

    },


    equals: function(other) {
        /// <summary>
        /// Compares this point with another instance.
        /// </summary>
        /// <param name="other" type="DeskApp.geometry.Point" >The point to compare with.</param>
        /// <returns type="Boolean">true if the point coordinates match; false otherwise.</returns>

    },


    getX: function() {
        /// <summary>
        /// Gets the x coordinate value.
        /// </summary>
        /// <returns type="Number">The current x coordinate value.</returns>

    },


    getY: function() {
        /// <summary>
        /// Gets the y coordinate value.
        /// </summary>
        /// <returns type="Number">The current y coordinate value.</returns>

    },


    move: function(x,y) {
        /// <summary>
        /// Moves the point to the specified x and y coordinates.
        /// </summary>
        /// <param name="x" type="Number" >The new X coordinate.</param>
        /// <param name="y" type="Number" >The new Y coordinate.</param>
        /// <returns type="DeskApp.geometry.Point">The current point instance.</returns>

    },


    rotate: function(angle,center) {
        /// <summary>
        /// Rotates the point around the given center.
        /// </summary>
        /// <param name="angle" type="Number" >Angle in decimal degrees. Measured in clockwise direction with 0 pointing "right". Negative values or values greater than 360 will be normalized.</param>
        /// <param name="center" type="Object" >The rotation center. Can be a Point instance or an [x, y] array.</param>
        /// <returns type="DeskApp.geometry.Point">The current Point instance.</returns>

    },


    round: function(digits) {
        /// <summary>
        /// Rounds the point coordinates to the specified number of fractional digits.
        /// </summary>
        /// <param name="digits" type="Number" >Number of fractional digits.</param>
        /// <returns type="DeskApp.geometry.Point">The current Point instance.</returns>

    },


    scale: function(scaleX,scaleY) {
        /// <summary>
        /// Scales the point coordinates along the x and y axis.
        /// </summary>
        /// <param name="scaleX" type="Number" >The x scale multiplier.</param>
        /// <param name="scaleY" type="Number" >The y scale multiplier.</param>
        /// <returns type="DeskApp.geometry.Point">The current point instance.</returns>

    },


    scaleCopy: function(scaleX,scaleY) {
        /// <summary>
        /// Scales the point coordinates on a copy of the current point.
/// The callee coordinates will remain unchanged.
        /// </summary>
        /// <param name="scaleX" type="Number" >The x scale multiplier.</param>
        /// <param name="scaleY" type="Number" >The y scale multiplier.</param>
        /// <returns type="DeskApp.geometry.Point">The new Point instance.</returns>

    },


    setX: function(value) {
        /// <summary>
        /// Sets the x coordinate to a new value.
        /// </summary>
        /// <param name="value" type="Number" >The new x coordinate value.</param>
        /// <returns type="DeskApp.geometry.Point">The current Point instance.</returns>

    },


    setY: function(value) {
        /// <summary>
        /// Sets the y coordinate to a new value.
        /// </summary>
        /// <param name="value" type="Number" >The new y coordinate value.</param>
        /// <returns type="DeskApp.geometry.Point">The current Point instance.</returns>

    },


    toArray: function(digits) {
        /// <summary>
        /// Returns the point coordinates as an [x, y] array.
        /// </summary>
        /// <param name="digits" type="Number" >(Optional) Number of fractional digits.</param>
        /// <returns type="Array">An array representation of the point, e.g. [10, 20]</returns>

    },


    toString: function(digits,separator) {
        /// <summary>
        /// Formats the point value to a string.
        /// </summary>
        /// <param name="digits" type="Number" >(Optional) Number of fractional digits.</param>
        /// <param name="separator" type="String" >The separator to place between coordinates.</param>
        /// <returns type="String">A string representation of the point, e.g. "10 20".</returns>

    },


    transform: function(tansformation) {
        /// <summary>
        /// Applies a transformation to the point coordinates.
/// The current coordinates will be overriden.
        /// </summary>
        /// <param name="tansformation" type="DeskApp.geometry.Transformation" >The transformation to apply.</param>
        /// <returns type="DeskApp.geometry.Point">The current Point instance.</returns>

    },


    transformCopy: function(tansformation) {
        /// <summary>
        /// Applies a transformation on a copy of the current point.
/// The callee coordinates will remain unchanged.
        /// </summary>
        /// <param name="tansformation" type="DeskApp.geometry.Transformation" >The transformation to apply.</param>
        /// <returns type="DeskApp.geometry.Point">The new Point instance.</returns>

    },


    translate: function(dx,dy) {
        /// <summary>
        /// Translates the point along the x and y axis.
        /// </summary>
        /// <param name="dx" type="Number" >The distance to move along the X axis.</param>
        /// <param name="dy" type="Number" >The distance to move along the Y axis.</param>
        /// <returns type="DeskApp.geometry.Point">The current point instance.</returns>

    },


    translateWith: function(vector) {
        /// <summary>
        /// Translates the point by using a Point instance as a vector of translation.
        /// </summary>
        /// <param name="vector" type="Object" >The vector of translation. Can be either a Point instance or an [x, y] array.</param>
        /// <returns type="DeskApp.geometry.Point">The current point instance.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppPoint = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.geometry.Point widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.geometry.Point">The DeskApp.geometry.Point instance (if present).</returns>
};

$.fn.DeskAppPoint = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.geometry.Point widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.geometry.Rect = function() { };

DeskApp.geometry.Rect.prototype = {




    bbox: function(matrix) {
        /// <summary>
        /// Returns the bounding box of this rectangle after applying the
/// specified transformation matrix.
        /// </summary>
        /// <param name="matrix" type="DeskApp.geometry.Matrix" >Transformation matrix to apply.</param>
        /// <returns type="DeskApp.geometry.Rect">The bounding box after applying the transformation matrix.</returns>

    },


    bottomLeft: function() {
        /// <summary>
        /// Gets the position of the bottom-left corner of the rectangle.
/// This is also the rectangle origin
        /// </summary>
        /// <returns type="DeskApp.geometry.Point">The position of the bottom-left corner.</returns>

    },


    bottomRight: function() {
        /// <summary>
        /// Gets the position of the bottom-right corner of the rectangle.
        /// </summary>
        /// <returns type="DeskApp.geometry.Point">The position of the bottom-right corner.</returns>

    },


    center: function() {
        /// <summary>
        /// Gets the position of the center of the rectangle.
        /// </summary>
        /// <returns type="DeskApp.geometry.Point">The position of the center.</returns>

    },


    clone: function() {
        /// <summary>
        /// Creates a new instance with the same origin and size.
        /// </summary>
        /// <returns type="DeskApp.geometry.Rect">A new Rect instance with the same origin and size.</returns>

    },


    equals: function(other) {
        /// <summary>
        /// Compares this rectangle with another instance.
        /// </summary>
        /// <param name="other" type="DeskApp.geometry.Rect" >The rectangle to compare with.</param>
        /// <returns type="Boolean">true if the origin and size is the same for both rectangles; false otherwise.</returns>

    },


    getOrigin: function() {
        /// <summary>
        /// Gets the origin (top-left point) of the rectangle.
        /// </summary>
        /// <returns type="DeskApp.geometry.Point">The origin (top-left point).</returns>

    },


    getSize: function() {
        /// <summary>
        /// Gets the rectangle size.
        /// </summary>
        /// <returns type="DeskApp.geometry.Size">The current rectangle Size.</returns>

    },


    height: function() {
        /// <summary>
        /// Gets the rectangle height.
        /// </summary>
        /// <returns type="Number">The rectangle height.</returns>

    },


    setOrigin: function(value) {
        /// <summary>
        /// Sets the origin (top-left point) of the rectangle.
        /// </summary>
        /// <param name="value" type="Object" >The new origin Point or equivalent [x, y] array.</param>
        /// <returns type="DeskApp.geometry.Rect">The current rectangle instance.</returns>

    },


    setSize: function(value) {
        /// <summary>
        /// Sets the rectangle size.
        /// </summary>
        /// <param name="value" type="Object" >The new rectangle Size or equivalent [width, height] array.</param>
        /// <returns type="DeskApp.geometry.Rect">The current rectangle instance.</returns>

    },


    topLeft: function() {
        /// <summary>
        /// Gets the position of the top-left corner of the rectangle.
/// This is also the rectangle origin
        /// </summary>
        /// <returns type="DeskApp.geometry.Point">The position of the top-left corner.</returns>

    },


    topRight: function() {
        /// <summary>
        /// Gets the position of the top-right corner of the rectangle.
        /// </summary>
        /// <returns type="DeskApp.geometry.Point">The position of the top-right corner.</returns>

    },


    width: function() {
        /// <summary>
        /// Gets the rectangle width.
        /// </summary>
        /// <returns type="Number">The rectangle width.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppRect = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.geometry.Rect widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.geometry.Rect">The DeskApp.geometry.Rect instance (if present).</returns>
};

$.fn.DeskAppRect = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.geometry.Rect widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.geometry.Size = function() { };

DeskApp.geometry.Size.prototype = {




    clone: function() {
        /// <summary>
        /// Creates a new instance with the same width and height.
        /// </summary>
        /// <returns type="DeskApp.geometry.Size">A new Size instance with the same coordinates.</returns>

    },


    equals: function(other) {
        /// <summary>
        /// Compares this Size with another instance.
        /// </summary>
        /// <param name="other" type="DeskApp.geometry.Size" >The Size to compare with.</param>
        /// <returns type="Boolean">true if the size members match; false otherwise.</returns>

    },


    getWidth: function() {
        /// <summary>
        /// Gets the width value.
        /// </summary>
        /// <returns type="Number">The current width value.</returns>

    },


    getHeight: function() {
        /// <summary>
        /// Gets the height value.
        /// </summary>
        /// <returns type="Number">The current height value.</returns>

    },


    setWidth: function(value) {
        /// <summary>
        /// Sets the width to a new value.
        /// </summary>
        /// <param name="value" type="Number" >The new width value.</param>
        /// <returns type="DeskApp.geometry.Size">The current Size instance.</returns>

    },


    setHeight: function(value) {
        /// <summary>
        /// Sets the height to a new value.
        /// </summary>
        /// <param name="value" type="Number" >The new height value.</param>
        /// <returns type="DeskApp.geometry.Size">The current Size instance.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppSize = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.geometry.Size widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.geometry.Size">The DeskApp.geometry.Size instance (if present).</returns>
};

$.fn.DeskAppSize = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.geometry.Size widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.geometry.Transformation = function() { };

DeskApp.geometry.Transformation.prototype = {




    clone: function() {
        /// <summary>
        /// Creates a new instance with the same transformation matrix.
        /// </summary>
        /// <returns type="DeskApp.geometry.Transformation">A new Transformation instance with the same matrix.</returns>

    },


    equals: function(other) {
        /// <summary>
        /// Compares this transformation with another instance.
        /// </summary>
        /// <param name="other" type="DeskApp.geometry.Transformation" >The transformation to compare with.</param>
        /// <returns type="Boolean">true if the transformation matrix is the same; false otherwise.</returns>

    },


    matrix: function() {
        /// <summary>
        /// Gets the current transformation matrix for this transformation.
        /// </summary>
        /// <returns type="DeskApp.geometry.Matrix">The current transformation matrix.</returns>

    },


    multiply: function(transformation) {
        /// <summary>
        /// Multiplies the transformation with another.
/// The underlying transformation matrix is updated in-place.
        /// </summary>
        /// <param name="transformation" type="DeskApp.geometry.Transformation" >The transformation to multiply by.</param>
        /// <returns type="DeskApp.geometry.Transformation">The current transformation instance.</returns>

    },


    rotate: function(angle,center) {
        /// <summary>
        /// Sets rotation with the specified parameters.
        /// </summary>
        /// <param name="angle" type="Number" >The angle of rotation in decimal degrees. Measured in clockwise direction with 0 pointing "right". Negative values or values greater than 360 will be normalized.</param>
        /// <param name="center" type="Object" >The center of rotation.</param>
        /// <returns type="DeskApp.geometry.Transformation">The current transformation instance.</returns>

    },


    scale: function(scaleX,scaleY) {
        /// <summary>
        /// Sets scale with the specified parameters.
        /// </summary>
        /// <param name="scaleX" type="Number" >The scale factor on the X axis.</param>
        /// <param name="scaleY" type="Number" >The scale factor on the Y axis.</param>
        /// <returns type="DeskApp.geometry.Transformation">The current transformation instance.</returns>

    },


    translate: function(x,y) {
        /// <summary>
        /// Sets translation with the specified parameters.
        /// </summary>
        /// <param name="x" type="Number" >The distance to translate along the X axis.</param>
        /// <param name="y" type="Number" >The distance to translate along the Y axis.</param>
        /// <returns type="DeskApp.geometry.Transformation">The current transformation instance.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppTransformation = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.geometry.Transformation widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.geometry.Transformation">The DeskApp.geometry.Transformation instance (if present).</returns>
};

$.fn.DeskAppTransformation = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.geometry.Transformation widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.Application = function() { };

DeskApp.mobile.Application.prototype = {




    changeLoadingMessage: function(text) {
        /// <summary>
        /// Changes the loading message.
        /// </summary>
        /// <param name="text" type="String" >New text of the loading animation.</param>

    },


    hideLoading: function() {
        /// <summary>
        /// Hide the loading animation.
        /// </summary>

    },


    navigate: function(url,transition) {
        /// <summary>
        /// Navigate to local or to remote view.
        /// </summary>
        /// <param name="url" type="String" >The id or url of the view.</param>
        /// <param name="transition" type="String" >Optional. The transition to apply when navigating. See View Transitions section for more information.</param>

    },


    replace: function(url,transition) {
        /// <summary>
        /// Navigate to local or to remote view. The view will replace the current one in the history stack.
        /// </summary>
        /// <param name="url" type="String" >The id or url of the view.</param>
        /// <param name="transition" type="String" >Optional. The transition to apply when navigating. See View Transitions section for more information.</param>

    },


    scroller: function() {
        /// <summary>
        /// Get a reference to the current view's scroller widget instance.
        /// </summary>
        /// <returns type="DeskApp.mobile.ui.Scroller">the scroller widget instance.</returns>

    },


    showLoading: function() {
        /// <summary>
        /// Show the loading animation.
        /// </summary>

    },


    skin: function(skin) {
        /// <summary>
        /// Change the current skin of the mobile application. When used without parameters, returns the currently used skin. Available as of Q2 2013.
        /// </summary>
        /// <param name="skin" type="String" >The skin name to switch to or empty string ("") to return to native.</param>
        /// <returns type="String">Current skin in effect.</returns>

    },


    view: function() {
        /// <summary>
        /// Get a reference to the current view.
        /// </summary>
        /// <returns type="DeskApp.mobile.ui.View">the view instance.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobileApplication = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.Application widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.Application">The DeskApp.mobile.Application instance (if present).</returns>
};

$.fn.DeskAppMobileApplication = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.Application widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;browserHistory — Boolean (default: true)
    /// &#10;Introduced in the 2014 Q3 release. If set to false, the navigation will not update or read the browser location fragment.
    /// &#10;
    /// &#10;hashBang — Boolean (default: false)
    /// &#10;Introduced in the 2014 Q1 Service Pack 1 release. If set to true, the navigation will parse and prefix the url fragment value with !,
/// &#10;which should be SEO friendly.
    /// &#10;
    /// &#10;hideAddressBar — Boolean (default: true)
    /// &#10;Whether to hide the browser address bar. Supported only in iPhone and iPod. Doesn't affect standalone mode as there the address bar is always hidden.
    /// &#10;
    /// &#10;initial — String 
    /// &#10;The id of the initial mobile View to display.
    /// &#10;
    /// &#10;layout — String 
    /// &#10;The id of the default Application layout.
    /// &#10;
    /// &#10;loading — String (default: ")
    /// &#10;The text displayed in the loading popup. Setting this value to false will disable the loading popup.Note: The text should be wrapped inside  tag.
    /// &#10;
    /// &#10;modelScope — Object (default: "window")
    /// &#10;The view model scope. By default, the views will try to resolve their models from the global scope (window).
    /// &#10;
    /// &#10;platform — String 
    /// &#10;Which platform look to force on the application. Supported values are "ios" (meaning iOS 6 look), "ios7","android", "blackberry" and "wp".
/// &#10;You can also set platform variants with it ("android-light" or "android-dark"), but keep in mind that it will still override the platform. If this is not desired, use the skin option.
    /// &#10;
    /// &#10;pushState — Boolean (default: false)
    /// &#10;If set to true, the application router instance will use the history pushState API.
    /// &#10;
    /// &#10;root — String (default: "/")
    /// &#10;Applicable if pushState is used and the application is deployed to a path different than /. If the application start page is hosted on http://foo.com/myapp/, the root option should be set to /myapp/.
    /// &#10;
    /// &#10;retina — Boolean (default: true)
    /// &#10;If set to true, the application will set the meta viewport tag scale value according to the device pixel ratio, and re-scale the app by setting root element font size to the respective value.
/// &#10;This will result in the widget borders/separators being real 1px  wide.
/// &#10;For example, in iPhone 4/5, the device pixel ratio is 2, which means that the scale will be set to 0.5, while the app root will receive a font-size: 2 * 0.92 inline style set.
    /// &#10;
    /// &#10;serverNavigation — Boolean 
    /// &#10;If set to true, the application will not use AJAX to load remote views.
    /// &#10;
    /// &#10;skin — String 
    /// &#10;The skin to apply to the application. Currently, DeskApp UI Mobile ships with nova, flat, material-light and material-dark skins in addition to the native looking ones.
/// &#10;You can also set platform variants with it ("android-light" or "android-dark").Note: The Material themes were renamed to material-light and material-dark in 2014 Q3 SP1. With 2014 Q3 (v2014.3.1119) and older DeskApp UI versions,
/// &#10;material and materialblack skin names should be used.
    /// &#10;
    /// &#10;statusBarStyle — String (default: "black")
    /// &#10;Set the status bar style meta tag in iOS used to control the styling of the status bar in a pinned to the Home Screen app. Available as of Q2 2013 SP.
    /// &#10;
    /// &#10;transition — String 
    /// &#10;The default View transition. For a list of supported transitions, check the Getting Started help topic.
    /// &#10;
    /// &#10;updateDocumentTitle — Boolean (default: true)
    /// &#10;Whether to update the document title.
    /// &#10;
    /// &#10;useNativeScrolling — Boolean (default: false)
    /// &#10;By default, the mobile application uses flexbox for the mobile views layout. The content element is made scrollable, either by initializing a mobile scroller or with the browser supported overflow: auto and -webkit-overflow-scrolling: touch CSS declarations.
/// &#10;When the useNativeScrolling configuration option is set to true, the view header and footer are positioned using position: fixed CSS declaration. The view content vertical padding is adjusted to match the header and footer height; The default browser scroller is utilized for the content scrolling.For more information regarding native scrolling check this article.
    /// &#10;
    /// &#10;webAppCapable — Boolean 
    /// &#10;Disables the default behavior of DeskApp UI Mobile apps to be web app capable (open in a chromeless browser). Introduced in Q2 2013.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.ui.ActionSheet = function() { };

DeskApp.mobile.ui.ActionSheet.prototype = {




    close: function() {
        /// <summary>
        /// Close the ActionSheet.
        /// </summary>

    },


    destroy: function() {
        /// <summary>
        /// Prepares the ActionSheet for safe removal from DOM. Detaches all event handlers and removes jQuery.data attributes to avoid memory leaks. Calls destroy method of any child DeskApp widgets.
        /// </summary>

    },


    open: function(target,context) {
        /// <summary>
        /// Open the ActionSheet.
        /// </summary>
        /// <param name="target" type="jQuery" >(optional) The target element of the ActionSheet, available in the callback methods.Notice The target element is mandatory on tablets, as the ActionSheet widget positions itself relative to opening element when a tablet is detected.</param>
        /// <param name="context" type="Object" >(optional) The context of the ActionSheet, available in the callback methods.</param>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobileActionSheet = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.ui.ActionSheet widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.ui.ActionSheet">The DeskApp.mobile.ui.ActionSheet instance (if present).</returns>
};

$.fn.DeskAppMobileActionSheet = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.ui.ActionSheet widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;cancel — String (default: "Cancel")
    /// &#10;The text of the cancel button.
    /// &#10;
    /// &#10;popup — Object 
    /// &#10;The popup configuration options (tablet only).
    /// &#10;
    /// &#10;type — String (default: auto)
    /// &#10;By default, the actionsheet opens as a full screen dialog on a phone device or as a popover if a tablet is detected. Setting the type to "phone" or "tablet" will force the looks of the widget regardless of the device.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.ui.BackButton = function() { };

DeskApp.mobile.ui.BackButton.prototype = {




    destroy: function() {
        /// <summary>
        /// Prepares the BackButton for safe removal from DOM. Detaches all event handlers and removes jQuery.data attributes to avoid memory leaks. Calls destroy method of any child DeskApp widgets.
        /// </summary>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobileBackButton = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.ui.BackButton widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.ui.BackButton">The DeskApp.mobile.ui.BackButton instance (if present).</returns>
};

$.fn.DeskAppMobileBackButton = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.ui.BackButton widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.ui.Button = function() { };

DeskApp.mobile.ui.Button.prototype = {




    badge: function(value) {
        /// <summary>
        /// Introduced in Q1 2013 SP Sets a badge on the Button with the specified value. If invoked without parameters, returns the current badge value. Set the value to false to remove the badge.
        /// </summary>
        /// <param name="value" type="Object" >The target value to be set or false to be removed.</param>
        /// <returns type="String | DeskApp.mobile.ui.Button">the badge value if invoked without parameters, otherwise the Button object.</returns>

    },


    destroy: function() {
        /// <summary>
        /// Prepares the Button for safe removal from DOM. Detaches all event handlers and removes jQuery.data attributes to avoid memory leaks. Calls destroy method of any child DeskApp widgets.
        /// </summary>

    },


    enable: function(enable) {
        /// <summary>
        /// Changes the enabled state of the widget.
        /// </summary>
        /// <param name="enable" type="Boolean" >Whether to enable or disable the widget.</param>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobileButton = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.ui.Button widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.ui.Button">The DeskApp.mobile.ui.Button instance (if present).</returns>
};

$.fn.DeskAppMobileButton = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.ui.Button widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;badge — String 
    /// &#10;The badge of the button.
    /// &#10;
    /// &#10;clickOn — String (default: default "up")
    /// &#10;Configures the DOM event used to trigger the button click event/navigate in the mobile application. Can be set to "down" as an alias for touchstart, mousedown, MSPointerDown, and PointerDown vendor specific events.
/// &#10;Setting the clickOn to down usually makes sense for buttons in the header or in non-scrollable views for increased responsiveness.By default, buttons trigger click/navigate when the user taps the button (a press + release action sequence occurs).
    /// &#10;
    /// &#10;enable — Boolean (default: true)
    /// &#10;If set to false the widget will be disabled and will not allow the user to click it. The widget is enabled by default.
    /// &#10;
    /// &#10;icon — String 
    /// &#10;The icon of the button. It can be either one of the built-in icons, or a custom one.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.ui.ButtonGroup = function() { };

DeskApp.mobile.ui.ButtonGroup.prototype = {




    badge: function(button,value) {
        /// <summary>
        /// Introduced in Q1 2013 SP Sets a badge on one of the ButtonGroup buttons with the specified value. If invoked without parameters, returns the button's current badge value. Set the value to false to remove the badge.
        /// </summary>
        /// <param name="button" type="Object" >The target button specified either as a jQuery selector/object or as an button index.</param>
        /// <param name="value" type="Object" >The target value to be set or false to be removed.</param>
        /// <returns type="String|DeskApp.mobile.ui.Button">the badge value if invoked without parameters, otherwise the ButtonGroup object.</returns>

    },


    current: function() {
        /// <summary>
        /// Get the currently selected Button.
        /// </summary>
        /// <returns type="jQuery">the jQuery object representing the currently selected button.</returns>

    },


    destroy: function() {
        /// <summary>
        /// Prepares the ButtonGroup for safe removal from DOM. Detaches all event handlers and removes jQuery.data attributes to avoid memory leaks. Calls destroy method of any child DeskApp widgets.
        /// </summary>

    },


    enable: function(enable) {
        /// <summary>
        /// Enables or disables the widget.
        /// </summary>
        /// <param name="enable" type="Boolean" >A boolean flag that indicates whether the widget should be enabled or disabled.</param>

    },


    select: function(li) {
        /// <summary>
        /// Select a Button.
        /// </summary>
        /// <param name="li" type="Object" >LI element or index of the Button.</param>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobileButtonGroup = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.ui.ButtonGroup widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.ui.ButtonGroup">The DeskApp.mobile.ui.ButtonGroup instance (if present).</returns>
};

$.fn.DeskAppMobileButtonGroup = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.ui.ButtonGroup widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;enable — Boolean (default: true)
    /// &#10;Defines if the widget is initially enabled or disabled.
    /// &#10;
    /// &#10;index — Number 
    /// &#10;Defines the initially selected Button (zero based index).
    /// &#10;
    /// &#10;selectOn — String (default: default "down")
    /// &#10;Sets the DOM event used to select the button. Accepts "up" as an alias for touchend, mouseup and MSPointerUp vendor specific events.By default, buttons are selected immediately after the user presses the button (on touchstart or mousedown or MSPointerDown, depending on the mobile device).
/// &#10;However, if the widget is placed in a scrollable view, the user may accidentally press the button when scrolling. In such cases, it is recommended to set this option to "up".
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.ui.Collapsible = function() { };

DeskApp.mobile.ui.Collapsible.prototype = {




    collapse: function(instant) {
        /// <summary>
        /// Collapses the content.
        /// </summary>
        /// <param name="instant" type="Boolean" >Optional. When set to true the collapse action will be performed without animation.</param>

    },


    destroy: function() {
        /// <summary>
        /// Prepares the Collapsible for safe removal from DOM. Detaches all event handlers and removes jQuery.data attributes to avoid memory leaks. Calls destroy method of any child DeskApp widgets.
        /// </summary>

    },


    expand: function(instant) {
        /// <summary>
        /// Expands the content.
        /// </summary>
        /// <param name="instant" type="Boolean" >When set to true the expand action will be performed without animation.</param>

    },


    resize: function() {
        /// <summary>
        /// Recalculates the content height.
        /// </summary>

    },


    toggle: function(instant) {
        /// <summary>
        /// Toggles the content visibility.
        /// </summary>
        /// <param name="instant" type="Boolean" >When set to true the expand/collapse action will be performed without animation.</param>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobileCollapsible = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.ui.Collapsible widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.ui.Collapsible">The DeskApp.mobile.ui.Collapsible instance (if present).</returns>
};

$.fn.DeskAppMobileCollapsible = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.ui.Collapsible widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;animation — Boolean (default: true)
    /// &#10;Turns on or off the animation of the widget.
    /// &#10;
    /// &#10;collapsed — Boolean (default: true)
    /// &#10;If set to false the widget content will be expanded initially. The content of the widget is collapsed by default.
    /// &#10;
    /// &#10;expandIcon — String (default: "plus")
    /// &#10;Sets the icon for the header of the collapsible widget when it is in a expanded state.
    /// &#10;
    /// &#10;iconPosition — String (default: "left")
    /// &#10;Sets the icon position in the header of the collapsible widget. Possible values are "left", "right", "top".
    /// &#10;
    /// &#10;inset — Boolean (default: "false")
    /// &#10;Forses inset appearance - the collapsible panel is padded from the View and receives rounded corners.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.ui.DetailButton = function() { };

DeskApp.mobile.ui.DetailButton.prototype = {




    destroy: function() {
        /// <summary>
        /// Prepares the DetailButton for safe removal from DOM. Detaches all event handlers and removes jQuery.data attributes to avoid memory leaks. Calls destroy method of any child DeskApp widgets.
        /// </summary>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobileDetailButton = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.ui.DetailButton widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.ui.DetailButton">The DeskApp.mobile.ui.DetailButton instance (if present).</returns>
};

$.fn.DeskAppMobileDetailButton = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.ui.DetailButton widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.ui.Drawer = function() { };

DeskApp.mobile.ui.Drawer.prototype = {




    destroy: function() {
        /// <summary>
        /// Prepares the Drawer for safe removal from DOM. Detaches all event handlers and removes jQuery.data attributes to avoid memory leaks. Calls destroy method of any child DeskApp widgets.
        /// </summary>

    },


    hide: function() {
        /// <summary>
        /// Hide the Drawer
        /// </summary>

    },


    show: function() {
        /// <summary>
        /// Show the Drawer
        /// </summary>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobileDrawer = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.ui.Drawer widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.ui.Drawer">The DeskApp.mobile.ui.Drawer instance (if present).</returns>
};

$.fn.DeskAppMobileDrawer = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.ui.Drawer widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;container — jQuery 
    /// &#10;Specifies the content element to shift when the drawer appears. Required if the drawer is used outside of a mobile application.
    /// &#10;
    /// &#10;position — String (default: 'left')
    /// &#10;The position of the drawer. Can be left (default) or right.
    /// &#10;
    /// &#10;swipeToOpen — Boolean (default: true)
    /// &#10;If set to false, swiping the view will not activate the drawer. In this case, the drawer will only be open by a designated button
    /// &#10;
    /// &#10;swipeToOpenViews — Array 
    /// &#10;A list of the view ids on which the drawer will appear when the view is swiped. If omitted, the swipe gesture will work on all views.
/// &#10;The option has effect only if swipeToOpen is set to true.
    /// &#10;
    /// &#10;title — String 
    /// &#10;The text to display in the Navbar title (if present).
    /// &#10;
    /// &#10;views — Array 
    /// &#10;A list of the view ids on which the drawer will appear. If omitted, the drawer will work on any view in the application.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.ui.Layout = function() { };

DeskApp.mobile.ui.Layout.prototype = {



    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobileLayout = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.ui.Layout widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.ui.Layout">The DeskApp.mobile.ui.Layout instance (if present).</returns>
};

$.fn.DeskAppMobileLayout = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.ui.Layout widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;id — String (default: null)
    /// &#10;The id of the layout. Required
    /// &#10;
    /// &#10;platform — String 
    /// &#10;The specific platform this layout targets. By default, layouts are displayed
/// &#10;on all platforms.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.ui.ListView = function() { };

DeskApp.mobile.ui.ListView.prototype = {




    append: function(dataItems) {
        /// <summary>
        /// Appends new items generated by rendering the given data items with the listview template to the bottom of the listview.
        /// </summary>
        /// <param name="dataItems" type="Array" ></param>

    },


    prepend: function(dataItems) {
        /// <summary>
        /// Prepends new items generated by rendering the given data items with the listview template to the top of the listview.
        /// </summary>
        /// <param name="dataItems" type="Array" ></param>

    },


    replace: function(dataItems) {
        /// <summary>
        /// Replaces the contents of the listview with the passed rendered data items.
        /// </summary>
        /// <param name="dataItems" type="Array" ></param>

    },


    remove: function(dataItems) {
        /// <summary>
        /// Removes the listview items which are rendered with the passed data items.
        /// </summary>
        /// <param name="dataItems" type="Array" ></param>

    },


    setDataItem: function(item,dataItem) {
        /// <summary>
        /// Re-renders the given listview item with the new dataItem provided. In order for the method to work as expected, the data items should be of type DeskApp.data.Model.
        /// </summary>
        /// <param name="item" type="jQuery" >The listview item to update</param>
        /// <param name="dataItem" type="DeskApp.data.Model" >The new dataItem</param>

    },


    destroy: function() {
        /// <summary>
        /// Prepares the ListView for safe removal from DOM. Detaches all event handlers and removes jQuery.data attributes to avoid memory leaks. Calls destroy method of any child DeskApp widgets.
        /// </summary>

    },


    items: function() {
        /// <summary>
        /// Get the listview DOM element items
        /// </summary>
        /// <returns type="jQuery">The listview DOM element items</returns>

    },


    refresh: function() {
        /// <summary>
        /// Repaints the listview (works only in databound mode).
        /// </summary>

    },


    setDataSource: function(dataSource) {
        /// <summary>
        /// Sets the DataSource of an existing ListView and rebinds it.
        /// </summary>
        /// <param name="dataSource" type="DeskApp.data.DataSource" ></param>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobileListView = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.ui.ListView widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.ui.ListView">The DeskApp.mobile.ui.ListView instance (if present).</returns>
};

$.fn.DeskAppMobileListView = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.ui.ListView widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;appendOnRefresh — Boolean (default: false)
    /// &#10;Used in combination with pullToRefresh. If set to true, newly loaded data will be appended on top when refreshing. Notice: not applicable if ListView is in a virtual mode.
    /// &#10;
    /// &#10;autoBind — Boolean (default: true)
    /// &#10;Indicates whether the listview will call read on the DataSource initially. If set to false, the listview will be bound after the DataSource instance fetch method is called.
    /// &#10;
    /// &#10;dataSource — DeskApp.data.DataSource|Object 
    /// &#10;Instance of DataSource or the data that the mobile ListView will be bound to.
    /// &#10;
    /// &#10;endlessScroll — Boolean (default: false)
    /// &#10;If set to true, the listview gets the next page of data when the user scrolls near the bottom of the view.
    /// &#10;
    /// &#10;fixedHeaders — Boolean (default: false)
    /// &#10;If set to true, the group headers will persist their position when the user scrolls through the listview.
/// &#10;Applicable only when the type is set to group, or when binding to grouped DataSource.Notice: fixed headers are not supported in virtual mode.
    /// &#10;
    /// &#10;headerTemplate — String|Function (default: "#:value#")
    /// &#10;The header item template (applicable when the type is set to group).
    /// &#10;
    /// &#10;loadMore — Boolean (default: false)
    /// &#10;If set to true, a button is rendered at the bottom of the listview. Tapping it fetches and displays the items from the next page of the DataSource.
    /// &#10;
    /// &#10;messages — Object 
    /// &#10;Defines the text of the ListView messages. Used primary for localization.
    /// &#10;
    /// &#10;pullToRefresh — Boolean (default: false)
    /// &#10;If set to true, the listview will reload its data when the user pulls the view over the top limit.
    /// &#10;
    /// &#10;pullParameters — Function 
    /// &#10;A callback function used when the 'pullToRefresh' option is enabled. The result of the function will be send as additional parameters to the DataSource's next method.Notice: When the listview is in a virtual mode, the pull to refresh action removes the previously loaded items in the listview (instead of appending new records at the top).
/// &#10;Previously loaded pages in the DataSource are also discarded.
    /// &#10;
    /// &#10;style — String (default: "")
    /// &#10;The style of the widget. Can be either empty string(""), or inset.
    /// &#10;
    /// &#10;template — String|Function (default: "#:data#")
    /// &#10;The item template.
    /// &#10;
    /// &#10;type — String (default: "flat")
    /// &#10;The type of the control. Can be either flat (default) or group. Determined automatically in databound mode.
    /// &#10;
    /// &#10;filterable — Boolean (default: false)
    /// &#10;Indicates whether the filter input must be visible or not.
    /// &#10;
    /// &#10;filterable — Object (default: false)
    /// &#10;Indicates whether the filter input must be visible or not.
    /// &#10;
    /// &#10;virtualViewSize — Number 
    /// &#10;Used when virtualization of local data is used. This configuration is needed to determine the items displayed, since the datasource does not (and should not) have paging set.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.ui.Loader = function() { };

DeskApp.mobile.ui.Loader.prototype = {




    hide: function() {
        /// <summary>
        /// Hide the loading animation.
        /// </summary>

    },


    show: function() {
        /// <summary>
        /// Show the loading animation.
        /// </summary>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobileLoader = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.ui.Loader widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.ui.Loader">The DeskApp.mobile.ui.Loader instance (if present).</returns>
};

$.fn.DeskAppMobileLoader = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.ui.Loader widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.ui.ModalView = function() { };

DeskApp.mobile.ui.ModalView.prototype = {




    close: function() {
        /// <summary>
        /// Close the ModalView
        /// </summary>

    },


    destroy: function() {
        /// <summary>
        /// Prepares the ModalView for safe removal from DOM. Detaches all event handlers and removes jQuery.data attributes to avoid memory leaks. Calls destroy method of any child DeskApp widgets.
        /// </summary>

    },


    open: function(target) {
        /// <summary>
        /// Open the ModalView
        /// </summary>
        /// <param name="target" type="jQuery" >(optional) The target of the ModalView</param>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobileModalView = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.ui.ModalView widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.ui.ModalView">The DeskApp.mobile.ui.ModalView instance (if present).</returns>
};

$.fn.DeskAppMobileModalView = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.ui.ModalView widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;height — Number 
    /// &#10;The height of the ModalView container in pixels. If not set, the element style is used.
    /// &#10;
    /// &#10;modal — Boolean (default: true)
    /// &#10;When set to false, the ModalView will close when the user taps outside of its element.
    /// &#10;
    /// &#10;width — Number 
    /// &#10;The width of the ModalView container in pixels. If not set, the element style is used.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.ui.NavBar = function() { };

DeskApp.mobile.ui.NavBar.prototype = {




    destroy: function() {
        /// <summary>
        /// Prepares the NavBar for safe removal from DOM. Detaches all event handlers and removes jQuery.data attributes to avoid memory leaks. Calls destroy method of any child DeskApp widgets.
        /// </summary>

    },


    title: function(value) {
        /// <summary>
        /// Update the title element text. The title element is specified by setting the role data attribute to view-title.
        /// </summary>
        /// <param name="value" type="String" >The text of title</param>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobileNavBar = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.ui.NavBar widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.ui.NavBar">The DeskApp.mobile.ui.NavBar instance (if present).</returns>
};

$.fn.DeskAppMobileNavBar = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.ui.NavBar widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.ui.Pane = function() { };

DeskApp.mobile.ui.Pane.prototype = {




    destroy: function() {
        /// <summary>
        /// Prepares the Pane for safe removal from DOM. Detaches all event handlers and removes jQuery.data attributes to avoid memory leaks. Calls destroy method of any child DeskApp widgets.
        /// </summary>

    },


    hideLoading: function() {
        /// <summary>
        /// Hide the loading animation.
        /// </summary>

    },


    navigate: function(url,transition) {
        /// <summary>
        /// Navigate the local or remote view.
        /// </summary>
        /// <param name="url" type="String" >The id or URL of the view.</param>
        /// <param name="transition" type="String" >The transition to apply when navigating. See View Transitions for more information.</param>

    },


    replace: function(url,transition) {
        /// <summary>
        /// Navigate to local or to remote view. The view will replace the current one in the history stack.
        /// </summary>
        /// <param name="url" type="String" >The id or URL of the view.</param>
        /// <param name="transition" type="String" >The transition to apply when navigating. See View Transitions for more information.</param>

    },


    Example: function() {
        /// <summary>
        /// 
        /// </summary>

    },


    showLoading: function() {
        /// <summary>
        /// Show the loading animation.
        /// </summary>

    },


    view: function() {
        /// <summary>
        /// Get a reference to the current view.
        /// </summary>
        /// <returns type="DeskApp.mobile.ui.View">the view instance.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobilePane = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.ui.Pane widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.ui.Pane">The DeskApp.mobile.ui.Pane instance (if present).</returns>
};

$.fn.DeskAppMobilePane = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.ui.Pane widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;collapsible — Boolean (default: false)
    /// &#10;Applicable when the pane is inside a SplitView. If set to true, the pane will be hidden when the device is in portrait position. The expandPanes SplitView method displays the hidden panes.The id of the initial mobile View to display.
    /// &#10;
    /// &#10;initial — String 
    /// &#10;The id of the initial mobile View to display.
    /// &#10;
    /// &#10;layout — String 
    /// &#10;The id of the default Pane Layout.
    /// &#10;
    /// &#10;loading — String (default: "Loading...")
    /// &#10;The text displayed in the loading popup. Setting this value to false will disable the loading popup.
    /// &#10;
    /// &#10;portraitWidth — Number 
    /// &#10;Sets the pane width in pixels when the device is in portrait position.
    /// &#10;
    /// &#10;transition — String 
    /// &#10;The default View transition.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.ui.PopOver = function() { };

DeskApp.mobile.ui.PopOver.prototype = {




    close: function() {
        /// <summary>
        /// Close the popover.
        /// </summary>

    },


    destroy: function() {
        /// <summary>
        /// Prepares the PopOver for safe removal from DOM. Detaches all event handlers and removes jQuery.data attributes to avoid memory leaks. Calls destroy method of any child DeskApp widgets.
        /// </summary>

    },


    open: function(target) {
        /// <summary>
        /// Open the PopOver.
        /// </summary>
        /// <param name="target" type="jQuery" >The target of the Popover, to which the visual arrow will point to. This parameter is required for a tablet OS.</param>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobilePopOver = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.ui.PopOver widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.ui.PopOver">The DeskApp.mobile.ui.PopOver instance (if present).</returns>
};

$.fn.DeskAppMobilePopOver = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.ui.PopOver widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;pane — Object 
    /// &#10;The pane configuration options.
    /// &#10;
    /// &#10;popup — Object 
    /// &#10;The popup configuration options.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.ui.ScrollView = function() { };

DeskApp.mobile.ui.ScrollView.prototype = {




    content: function(content) {
        /// <summary>
        /// Update the ScrollView HTML content.
        /// </summary>
        /// <param name="content" type="Object" >The new ScrollView content.</param>

    },


    destroy: function() {
        /// <summary>
        /// Prepares the ScrollView for safe removal from DOM. Detaches all event handlers and removes jQuery.data attributes to avoid memory leaks. Calls destroy method of any child DeskApp widgets.
        /// </summary>

    },


    next: function() {
        /// <summary>
        /// Switches to the next page with animation.
        /// </summary>

    },


    prev: function() {
        /// <summary>
        /// Switches to the previous page with animation.
        /// </summary>

    },


    refresh: function() {
        /// <summary>
        /// Redraw the mobile ScrollView pager. Called automatically on device orientation change event.
        /// </summary>

    },


    scrollTo: function(page,instant) {
        /// <summary>
        /// Scroll to the given page. Pages are zero-based indexed.
        /// </summary>
        /// <param name="page" type="Number" >The page to scroll to.</param>
        /// <param name="instant" type="Boolean" >If set to true, the ScrollView will jump instantly to the given page without any animation effects.</param>

    },


    setDataSource: function(dataSource) {
        /// <summary>
        /// Sets the DataSource of an existing ScrollView and rebinds it.
        /// </summary>
        /// <param name="dataSource" type="DeskApp.data.DataSource" ></param>

    },


    value: function(dataItem) {
        /// <summary>
        /// Works in data-bound mode only. If a parameter is passed, the widget scrolls to the given dataItem. If not, the method return currently displayed dataItem.
        /// </summary>
        /// <param name="dataItem" type="Object" >The dataItem to set.</param>
        /// <returns type="Object">The currently displayed dataItem.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobileScrollView = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.ui.ScrollView widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.ui.ScrollView">The DeskApp.mobile.ui.ScrollView instance (if present).</returns>
};

$.fn.DeskAppMobileScrollView = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.ui.ScrollView widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;autoBind — Boolean (default: true)
    /// &#10;If set to false the widget will not bind to the DataSource during initialization. In this case data binding will occur when the change event of the data source is fired. By default the widget will bind to the DataSource specified in the configuration.Applicable only in data bound mode.
    /// &#10;
    /// &#10;bounceVelocityThreshold — Number (default: 1.6)
    /// &#10;The velocity threshold after which a swipe will result in a bounce effect.
    /// &#10;
    /// &#10;contentHeight — Number|String (default: "auto")
    /// &#10;The height of the ScrollView content. Supports 100% if the ScrollView is embedded in a stretched view and the ScrollView element is an immediate child of the view element.
    /// &#10;
    /// &#10;dataSource — DeskApp.data.DataSource|Object 
    /// &#10;Instance of DataSource that the mobile ScrollView will be bound to. If DataSource is set, the widget will operate in data bound mode.
    /// &#10;
    /// &#10;duration — Number (default: 400)
    /// &#10;The milliseconds that take the ScrollView to snap to the current page after released.
    /// &#10;
    /// &#10;emptyTemplate — String (default: "")
    /// &#10;The template which is used to render the pages without content. By default the ScrollView renders a blank page.Applicable only in data bound mode.
    /// &#10;
    /// &#10;enablePager — Boolean (default: true)
    /// &#10;If set to true the ScrollView will display a pager. By default pager is enabled.
    /// &#10;
    /// &#10;itemsPerPage — Number (default: 1)
    /// &#10;Determines how many data items will be passed to the page template.Applicable only in data bound mode.
    /// &#10;
    /// &#10;page — Number (default: 0)
    /// &#10;The initial page to display.
    /// &#10;
    /// &#10;pageSize — Number (default: 1)
    /// &#10;Multiplier applied to the snap amount of the ScrollView. By default, the widget scrolls to the next screen when swipe. If the pageSize property is set to 0.5, the ScrollView will scroll by half of the widget width.Not applicable in data bound mode.
    /// &#10;
    /// &#10;template — String (default: "#:data#")
    /// &#10;The template which is used to render the content of pages. By default the ScrollView renders a div element for every page.Applicable only in data bound mode.
    /// &#10;
    /// &#10;velocityThreshold — Number (default: 0.8)
    /// &#10;The velocity threshold after which a swipe will navigate to the next page (as opposed to snapping back to the current page).
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.ui.Scroller = function() { };

DeskApp.mobile.ui.Scroller.prototype = {




    animatedScrollTo: function(x,y) {
        /// <summary>
        /// Scrolls the scroll container to the specified location with animation. The arguments should be negative numbers.
        /// </summary>
        /// <param name="x" type="Number" >The horizontal offset in pixels to scroll to.</param>
        /// <param name="y" type="Number" >The vertical offset in pixels to scroll to.</param>

    },


    contentResized: function() {
        /// <summary>
        /// Updates the scroller dimensions. Should be called after the contents of the scroller update their size
        /// </summary>

    },


    destroy: function() {
        /// <summary>
        /// Prepares the Scroller for safe removal from DOM. Detaches all event handlers and removes jQuery.data attributes to avoid memory leaks. Calls destroy method of any child DeskApp widgets.
        /// </summary>

    },


    disable: function() {
        /// <summary>
        /// Disables the scrolling of the element.
        /// </summary>

    },


    enable: function() {
        /// <summary>
        /// Enables the scrolling of the element after it has been disabled by calling disable.
        /// </summary>

    },


    height: function() {
        /// <summary>
        /// Returns the viewport height of the scrollable element.
        /// </summary>
        /// <returns type="Number">the viewport height in pixels.</returns>

    },


    pullHandled: function() {
        /// <summary>
        /// Indicate that the pull event is handled (i.e. data from the server has been retrieved).
        /// </summary>

    },


    reset: function() {
        /// <summary>
        /// Scrolls the container to the top.
        /// </summary>

    },


    scrollHeight: function() {
        /// <summary>
        /// Returns the height in pixels of the scroller content.
        /// </summary>

    },


    scrollTo: function(x,y) {
        /// <summary>
        /// Scrolls the container to the specified location. The arguments should be negative numbers.
        /// </summary>
        /// <param name="x" type="Number" >The horizontal offset in pixels to scroll to.</param>
        /// <param name="y" type="Number" >The vertical offset in pixels to scroll to.</param>

    },


    scrollWidth: function() {
        /// <summary>
        /// Returns the width in pixels of the scroller content.
        /// </summary>

    },


    zoomOut: function() {
        /// <summary>
        /// Zooms the scroller out to the minimum zoom level possible.
        /// </summary>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobileScroller = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.ui.Scroller widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.ui.Scroller">The DeskApp.mobile.ui.Scroller instance (if present).</returns>
};

$.fn.DeskAppMobileScroller = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.ui.Scroller widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;elastic — Boolean (default: true)
    /// &#10;Weather or not to allow out of bounds dragging and easing.
    /// &#10;
    /// &#10;messages — Object 
    /// &#10;Defines the text of the Scroller pull to refresh messages. Used primary for localization.
    /// &#10;
    /// &#10;pullOffset — Number (default: 140)
    /// &#10;The threshold below which releasing the scroller will trigger the pull event.
/// &#10;Has effect only when the pullToRefresh option is set to true.
    /// &#10;
    /// &#10;pullToRefresh — Boolean (default: false)
    /// &#10;If set to true, the scroller will display a hint when the user pulls the container beyond its top limit.
/// &#10;If a pull beyond the specified pullOffset occurs, a pull event will be triggered.
    /// &#10;
    /// &#10;useNative — Boolean (default: false)
    /// &#10;If set to true, the scroller will use the native scrolling available in the current platform. This should help with form issues on some platforms (namely Android and WP8).
/// &#10;Native scrolling is only enabled on platforms that support it: iOS > 4, Android > 2, WP8. BlackBerry devices do support it, but the native scroller is flaky.
    /// &#10;
    /// &#10;visibleScrollHints — Boolean (default: false)
    /// &#10;If set to true, the scroller scroll hints will always be displayed.
    /// &#10;
    /// &#10;zoom — Boolean (default: false)
    /// &#10;If set to true, the user can zoom in/out the contents of the widget using the pinch/zoom gesture.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.ui.SplitView = function() { };

DeskApp.mobile.ui.SplitView.prototype = {




    destroy: function() {
        /// <summary>
        /// Prepares the SplitView for safe removal from DOM. Detaches all event handlers and removes jQuery.data attributes to avoid memory leaks. Calls destroy method of any child DeskApp widgets.
        /// </summary>

    },


    expandPanes: function() {
        /// <summary>
        /// Displays the collapsible panes; has effect only when the device is in portrait orientation.
        /// </summary>

    },


    collapsePanes: function() {
        /// <summary>
        /// Collapses back the collapsible panes (displayed previously with expandPanes); has effect only when the device is in portrait orientation.
        /// </summary>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobileSplitView = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.ui.SplitView widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.ui.SplitView">The DeskApp.mobile.ui.SplitView instance (if present).</returns>
};

$.fn.DeskAppMobileSplitView = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.ui.SplitView widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;style — String (default: "horizontal")
    /// &#10;Defines the SplitView style - horizontal or vertical.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.ui.Switch = function() { };

DeskApp.mobile.ui.Switch.prototype = {




    check: function(check) {
        /// <summary>
        /// Get/Set the checked state of the widget.
        /// </summary>
        /// <param name="check" type="Boolean" >Whether to turn the widget on or off.</param>
        /// <returns type="Boolean">The checked state of the widget.</returns>

    },


    destroy: function() {
        /// <summary>
        /// Prepares the Switch for safe removal from DOM. Detaches all event handlers and removes jQuery.data attributes to avoid memory leaks. Calls destroy method of any child DeskApp widgets.
        /// </summary>

    },


    enable: function(enable) {
        /// <summary>
        /// Changes the enabled state of the widget.
        /// </summary>
        /// <param name="enable" type="Boolean" >Whether to enable or disable the widget.</param>

    },


    refresh: function() {
        /// <summary>
        /// Forces the Switch to recalculate its dimensions. Useful when major changes in the interface happen dynamically, like for instance changing the skin.
        /// </summary>

    },


    toggle: function() {
        /// <summary>
        /// Toggle the checked state of the widget.
        /// </summary>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobileSwitch = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.ui.Switch widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.ui.Switch">The DeskApp.mobile.ui.Switch instance (if present).</returns>
};

$.fn.DeskAppMobileSwitch = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.ui.Switch widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;checked — Boolean (default: false)
    /// &#10;The checked state of the widget.
    /// &#10;
    /// &#10;enable — Boolean (default: true)
    /// &#10;If set to false the widget will be disabled and will not allow the user to change its checked state. The widget is enabled by default.
    /// &#10;
    /// &#10;offLabel — String (default: "OFF")
    /// &#10;The OFF label.
    /// &#10;
    /// &#10;onLabel — String (default: "ON")
    /// &#10;The ON label.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.ui.TabStrip = function() { };

DeskApp.mobile.ui.TabStrip.prototype = {




    badge: function(tab,value) {
        /// <summary>
        /// Introduced in Q1 2013 SP Sets a badge on one of the tabs with the specified value. If invoked without second parameter, returns the tab's current badge value. Set the value to false to remove the badge.
        /// </summary>
        /// <param name="tab" type="Object" >The target tab specified either as a jQuery selector/object or as an item index.</param>
        /// <param name="value" type="Object" >The target value to be set or false to be removed.</param>
        /// <returns type="String|DeskApp.mobile.ui.TabStrip">Returns the badge value if invoked without parameters, otherwise returns the TabStrip object.</returns>

    },


    currentItem: function() {
        /// <summary>
        /// Get the currently selected tab DOM element.
        /// </summary>
        /// <returns type="jQuery">the currently selected tab DOM element.</returns>

    },


    destroy: function() {
        /// <summary>
        /// Prepares the TabStrip for safe removal from DOM. Detaches all event handlers and removes jQuery.data attributes to avoid memory leaks. Calls destroy method of any child DeskApp widgets.
        /// </summary>

    },


    switchTo: function(url) {
        /// <summary>
        /// Set the mobile TabStrip active tab to the tab with the specified URL. This method doesn't change the current View. To change the View, use Application's navigate method instead.
        /// </summary>
        /// <param name="url" type="Object" >The URL or zero based index of the tab.</param>

    },


    switchByFullUrl: function(url) {
        /// <summary>
        /// Set the mobile TabStrip active tab to the tab with the specified full URL. This method doesn't change the current View. To change the View, use Application's navigate method instead.
        /// </summary>
        /// <param name="url" type="String" >The URL of the tab.</param>

    },


    clear: function() {
        /// <summary>
        /// Clear the currently selected tab.
        /// </summary>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobileTabStrip = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.ui.TabStrip widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.ui.TabStrip">The DeskApp.mobile.ui.TabStrip instance (if present).</returns>
};

$.fn.DeskAppMobileTabStrip = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.ui.TabStrip widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;selectedIndex — Number (default: 0)
    /// &#10;The index of the initially selected tab.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.ui.View = function() { };

DeskApp.mobile.ui.View.prototype = {




    contentElement: function() {
        /// <summary>
        /// Retrieves the current content holder of the View - this is the content element if the View is stretched or the scroll container otherwise.
        /// </summary>

    },


    destroy: function() {
        /// <summary>
        /// Prepares the View for safe removal from DOM. Detaches all event handlers and removes jQuery.data attributes to avoid memory leaks. Calls destroy method of any child DeskApp widgets.
        /// </summary>

    },


    enable: function(enable) {
        /// <summary>
        /// Enables or disables the user interaction with the view and its contents.
        /// </summary>
        /// <param name="enable" type="Boolean" >Omitting the parameter or passing true enables the view. Passing false disables the view.</param>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobileView = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.ui.View widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.ui.View">The DeskApp.mobile.ui.View instance (if present).</returns>
};

$.fn.DeskAppMobileView = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.ui.View widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;model — String (default: null)
    /// &#10;The MVVM model to bind to. If a string is passed, The view will try to resolve a reference to the view model variable in the global scope.
    /// &#10;
    /// &#10;reload — Boolean (default: false)
    /// &#10;Applicable to remote views only. If set to true, the remote view contents will be reloaded from the server (using Ajax) each time the view is navigated to.
    /// &#10;
    /// &#10;scroller — Object (default: null)
    /// &#10;Configuration options to be passed to the scroller instance instantiated by the view. For more details, check the scroller configuration options.
    /// &#10;
    /// &#10;stretch — Boolean (default: false)
    /// &#10;If set to true, the view will stretch its child contents to occupy the entire view, while disabling kinetic scrolling.
/// &#10;Useful if the view contains an image or a map.
    /// &#10;
    /// &#10;title — String 
    /// &#10;The text to display in the NavBar title (if present) and the browser title.
    /// &#10;
    /// &#10;useNativeScrolling — Boolean (default: false)
    /// &#10;If set to true, the view will use the native scrolling available in the current platform. This should help with form issues on some platforms (namely Android and WP8).
/// &#10;Native scrolling is only enabled on platforms that support it: iOS > 5+, Android > 3+, WP8. BlackBerry devices do support it, but the native scroller is flaky.
    /// &#10;
    /// &#10;zoom — Boolean (default: false)
    /// &#10;If set to true, the user can zoom in/out the contents of the view using the pinch/zoom gesture.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.mobile.ui.Widget = function() { };

DeskApp.mobile.ui.Widget.prototype = {




    view: function() {
        /// <summary>
        /// Returns the DeskApp.mobile.ui.View which contains the widget. If the widget is contained in a splitview, modalview, or drawer, the respective widget instance is returned.
        /// </summary>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppMobileWidget = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.mobile.ui.Widget widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.mobile.ui.Widget">The DeskApp.mobile.ui.Widget instance (if present).</returns>
};

$.fn.DeskAppMobileWidget = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.mobile.ui.Widget widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.ooxml.Workbook = function() { };

DeskApp.ooxml.Workbook.prototype = {




    toDataURL: function() {
        /// <summary>
        /// Creates an Excel file that represents the current workbook and returns it as a data URL.
        /// </summary>
        /// <returns type="String">the Excel file as data URL.</returns>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppWorkbook = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.ooxml.Workbook widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.ooxml.Workbook">The DeskApp.ooxml.Workbook instance (if present).</returns>
};

$.fn.DeskAppWorkbook = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.ooxml.Workbook widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;creator — String (default: "DeskApp UI")
    /// &#10;The creator of the workbook.
    /// &#10;
    /// &#10;date — Date 
    /// &#10;The date when the workbook is created. The default value is new Date().
    /// &#10;
    /// &#10;sheets — Array 
    /// &#10;The sheets of the workbook. Every sheet represents a page from the final Excel file.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};


DeskApp.ui.Touch = function() { };

DeskApp.ui.Touch.prototype = {




    cancel: function() {
        /// <summary>
        /// Cancels the current touch event sequence. Calling cancel in a touchstart or dragmove will disable subsequent move or tap/end/hold event handlers from being called.
        /// </summary>

    },


    destroy: function() {
        /// <summary>
        /// Prepares the Touch for safe removal from DOM. Detaches all event handlers and removes jQuery.data attributes to avoid memory leaks. Calls destroy method of any child DeskApp widgets.
        /// </summary>

    },

    bind: function(event, callback) {
        /// <summary>
        /// Binds to a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be executed when the event is triggered.</param>
    },

    unbind: function(event, callback) {
        /// <summary>
        /// Unbinds a callback from a widget event.
        /// </summary>
        /// <param name="event" type="String">The event name</param>
        /// <param name="callback" type="Function">The callback to be removed.</param>
    }

};

$.fn.getDeskAppTouch = function() {
    /// <summary>
    /// Returns a reference to the DeskApp.ui.Touch widget, instantiated on the selector.
    /// </summary>
    /// <returns type="DeskApp.ui.Touch">The DeskApp.ui.Touch instance (if present).</returns>
};

$.fn.DeskAppTouch = function(options) {
    /// <summary>
    /// Instantiates a DeskApp.ui.Touch widget based the DOM elements that match the selector.

    /// &#10;Accepts an object with the following configuration options:
    /// &#10;
    /// &#10;filter — String 
    /// &#10;jQuery selector that specifies child elements that are touchable if a widget is attached to a container.
    /// &#10;
    /// &#10;surface — jQuery (default: null)
    /// &#10;If specified, the user drags will be tracked within the surface boundaries.
/// &#10;This option is useful if the widget is instantiated on small DOM elements like buttons, or thin list items.
    /// &#10;
    /// &#10;multiTouch — Boolean (default: false)
    /// &#10;If set to true, the widget will capture and trigger the gesturestart, gesturechange, and gestureend events when the user touches the element with two fingers.
    /// &#10;
    /// &#10;enableSwipe — Boolean (default: false)
    /// &#10;If set to true, the Touch widget will recognize horizontal swipes and trigger the swipe event.Notice: if the enableSwipe option is set to true, the dragstart, drag and dragend events will not be triggered.
    /// &#10;
    /// &#10;minXDelta — Number (default: 30)
    /// &#10;The minimum horizontal distance in pixels the user should swipe before the swipe event is triggered.
    /// &#10;
    /// &#10;maxYDelta — Number (default: 20)
    /// &#10;The maximum vertical deviation in pixels of the swipe event. Swipes with higher deviation are discarded.
    /// &#10;
    /// &#10;maxDuration — Number (default: 1000)
    /// &#10;The maximum amount of time in milliseconds the swipe event can last. Slower swipes are discarded.
    /// &#10;
    /// &#10;minHold — Number (default: 800)
    /// &#10;The timeout in milliseconds before the hold event is fired.Notice: the hold event will be triggered after the time passes, not after the user lifts his/hers finger.
    /// &#10;
    /// &#10;doubleTapTimeout — Number (default: 400)
    /// &#10;The maximum period (in milliseconds) between two consecutive taps which will trigger the doubletap event.
    /// &#10;
    /// </summary>
    /// <param name="options" type="Object">
    /// The widget configuration options
    /// </param>
};

