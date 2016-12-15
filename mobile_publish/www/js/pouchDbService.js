angular.module('myapp').service('pouchDbService', function ($rootScope) {
        var dbInBrowser;
        var remoteDb;
        //var sync = null;
        this.setDatabase = function (dbName, hostDb) {            
            dbInBrowser = new PouchDB(dbName);
            remoteDb = new PouchDB(hostDb)
            dbInBrowser.sync(remoteDb, { live: true, retry: true })
        }

        this.putDoc = function (doc) {
            var putDocPromise = new Promise(function (resolve, reject) {                
                if (!doc._id) {
                    dbInBrowser.put(doc).then(function (res) {
                        resolve(res);
                    }).catch(function (err) {
                        reject(err)
                        console.log(err);
                    });
                } else {
                    dbInBrowser.put(doc).then(function (res) {
                        resolve(res);
                    }).catch(function (err) {
                        if (err.name == 'conflict') {
                            dbInBrowser.get(doc._id).then(function (objDoc) {
                                dbInBrowser.remove(objDoc._id, objDoc._rev).then(function (success) {
                                    dbInBrowser.put(doc).then(function (resp) {
                                        resolve(resp);
                                    }).catch(function (err) {
                                        pouchDbService.putDoc(doc);
                                        console.log(err);
                                    })
                                })
                            })
                        }
                        console.log(err);
                    });            
                }
            });
            return putDocPromise;
        }

        this.removeById = function (doc) {
            var removePromise = new Promise(function (resolve, reject) {
                dbInBrowser.remove(doc).then(function (result) {
                    resolve(result);
                }).catch(function (err) {
                    console.log(err);
                });

            });
            return removePromise;
        }

        this.query = function (map, options) {
            var queryPromise = new Promise(function (resolve, reject) {
                dbInBrowser.query(map, options).then(function (result) {
                    resolve(result);
                }).catch(function (err) {
                    console.log(err);
                });
            });
            return queryPromise;
        }

        this.deleteDoc = function (documentId, documentRevision) {
            return database.remove(documentId, documentRevision);
        }

        this.getDocumentById = function (documentId) {
            var getDocumentPromise = new Promise(function (resolve, reject) {
                dbInBrowser.get(documentId).then(function (doc) {                    
                    resolve(doc);                    
                }).catch(function (err) {
                    console.log(err);
                });
            });  
            return getDocumentPromise;
            
        }

        this.queryById = function (options) {
            var queryPromise = new Promise(function (resolve, reject) {
                dbInBrowser.allDocs(options).then(function (result) {
                    var objdocs = [];
                    result.rows.forEach(function (entry) {
                        objdocs.push(entry.doc);
                    });
                    resolve(objdocs);

                }).catch(function (err) {
                    console.log(err);
                });
            });
            return queryPromise;
        }
});
    
