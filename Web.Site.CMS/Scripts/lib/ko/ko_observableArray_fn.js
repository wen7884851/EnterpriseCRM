ko.observableArray.fn.filterByProperty = function (propName, matchValue) {
    return ko.pureComputed(function () {
        var allItems = this(), matchingItems = [];
        for (var _i = 0, allItems_1 = allItems; _i < allItems_1.length; _i++) {
            var item = allItems_1[_i];
            if (item && item[propName] && ko.unwrap(item[propName]) === matchValue) {
                matchingItems.push(item);
            }
        }
        return matchingItems;
    }, this);
};
ko.observableArray.fn.filter = function (predicate) {
    return ko.pureComputed(function () {
        var allItems = this(), matchingItems = [];
        var index = 0;
        for (var _i = 0, allItems_2 = allItems; _i < allItems_2.length; _i++) {
            var item = allItems_2[_i];
            if (predicate(item, index, allItems)) {
                matchingItems.push(item);
            }
            index = index + 1;
        }
        return matchingItems;
    }, this);
};
ko.observableArray.fn.each = function (action) {
    var allItems = this();
    for (var _i = 0, allItems_3 = allItems; _i < allItems_3.length; _i++) {
        var item = allItems_3[_i];
        action(item);
    }
};
ko.observableArray.fn.emptyThenPushAll = function (valuesToPush) {
    var self = this;
    var underlyingArray = self();
    self.removeAll();
    if (valuesToPush) {
        this.valueWillMutate();
        ko.utils.arrayPushAll(underlyingArray, valuesToPush);
        this.valueHasMutated();
    }
    return this;
};
ko.observableArray.fn.pushAll = function (valuesToPush) {
    if (valuesToPush && valuesToPush.length > 0) {
        var underlyingArray = this();
        this.valueWillMutate();
        ko.utils.arrayPushAll(underlyingArray, valuesToPush);
        this.valueHasMutated();
    }
    return this;
};
ko.observableArray.fn.removeByIndex = function (index) {
    var self = this;
    if (self().length > 0 && self().length - 1 >= index) {
        self.splice(index, 1);
    }
    return self;
};
ko.observableArray.fn.removeByItem = function (item) {
    var items = this;
    var index = 0;
    for (var _i = 0, _a = items(); _i < _a.length; _i++) {
        var i = _a[_i];
        if (i === item) {
            items.splice(index, 1);
            break;
        }
        index++;
    }
    return self;
};
ko.observableArray.fn.first = function (predicate) {
    var allItems = this(), matchingItem = null;
    var index = 0;
    for (var _i = 0, allItems_4 = allItems; _i < allItems_4.length; _i++) {
        var item = allItems_4[_i];
        if (predicate(item, index, allItems)) {
            matchingItem = item;
            break;
        }
        index = index + 1;
    }
    return matchingItem;
};
ko.observableArray.fn.index = function (predicate) {
    var allItems = this();
    var index = 0;
    for (var _i = 0, allItems_5 = allItems; _i < allItems_5.length; _i++) {
        var item = allItems_5[_i];
        if (predicate(item, index, allItems)) {
            return index;
        }
        index = index + 1;
    }
    return -1;
};
ko.observableArray.fn.mappingfromArray = function (array) {
    var _this = this;
    _.each(array, function (item) {
        _this.push(ko.mapping.fromJS(item, item));
    });
};
//# sourceMappingURL=ko_observableArray_fn.js.map