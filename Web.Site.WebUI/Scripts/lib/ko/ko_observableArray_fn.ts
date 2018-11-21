interface KnockoutObservableArrayFunctions<T> {
    filter(predicate: any): KnockoutComputed<Array<T>>;
    filterByProperty(propName: string, matchValue: any): KnockoutComputed<Array<T>>;
    each(action: (item: T) => void): void;
    pushAll(valuesToPush: T[]): void;
    emptyThenPushAll(valuesToPush: T[]): void;
    first(predicate: any): T;
    index(predicate: any): number;
    removeByIndex(index: number): void;
    removeByItem(item: any): void;
    mappingfromArray(array: Object[]): void;
}

ko.observableArray.fn.filterByProperty = function (propName: string, matchValue: any) {
    return ko.pureComputed(function () {
        var allItems = this(), matchingItems = [];
        for (var item of allItems) {
            if (item && item[propName] && ko.unwrap(item[propName]) === matchValue) {
                matchingItems.push(item);
            }
        }
        return matchingItems;
    }, this);
}

ko.observableArray.fn.filter = function (predicate: any) {
    return ko.pureComputed(function () {
        var allItems = this(), matchingItems = [];
        var index = 0;
        for (var item of allItems) {
            if (predicate(item, index, allItems)) {
                matchingItems.push(item);
            }
            index = index + 1;
        }
        return matchingItems;
    }, this);
}

ko.observableArray.fn.each = function (action) {
    var allItems = this();
    for (var item of allItems) {
        action(item);
    }
}

ko.observableArray.fn.emptyThenPushAll = function (valuesToPush: Array<any>) {
    var self = <KnockoutObservableArray<any>>this;
    var underlyingArray = self();

    self.removeAll();

    if (valuesToPush) {
        this.valueWillMutate();
        ko.utils.arrayPushAll(underlyingArray, valuesToPush);
        this.valueHasMutated();
    }
    return this;
}

ko.observableArray.fn.pushAll = function (valuesToPush: Array<any>) {
    if (valuesToPush && valuesToPush.length > 0) {
        var underlyingArray = this();
        this.valueWillMutate();
        ko.utils.arrayPushAll(underlyingArray, valuesToPush);
        this.valueHasMutated();
    }

    return this;
}

ko.observableArray.fn.removeByIndex = function (index: number) {
    var self = <KnockoutObservableArray<any>>this;
    if (self().length > 0 && self().length - 1 >= index) {
        self.splice(index, 1);
    }
    return self;
}

ko.observableArray.fn.removeByItem = function (item: any) {
    var items = <KnockoutObservableArray<any>>this;
    let index = 0;
    for (let i of items()) {
        if (i === item) {
            items.splice(index, 1);
            break;
        }
        index++;
    }
    return self;
}

ko.observableArray.fn.first = function (predicate: any) {
    var allItems = this(), matchingItem = null;
    var index = 0;
    for (var item of allItems) {
        if (predicate(item, index, allItems)) {
            matchingItem = item;
            break;
        }
        index = index + 1;
    }
    return matchingItem;
}

ko.observableArray.fn.index = function (predicate: any) {
    var allItems = this();
    var index = 0;
    for (var item of allItems) {
        if (predicate(item, index, allItems)) {
            return index;
        }
        index = index + 1;
    }
    return -1;
}

ko.observableArray.fn.mappingfromArray = function (array: Object[]) {
    _.each(array, (item) => {
        this.push(ko.mapping.fromJS(item, item));
    });
}