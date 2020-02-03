/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, { enumerable: true, get: getter });
/******/ 		}
/******/ 	};
/******/
/******/ 	// define __esModule on exports
/******/ 	__webpack_require__.r = function(exports) {
/******/ 		if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 			Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 		}
/******/ 		Object.defineProperty(exports, '__esModule', { value: true });
/******/ 	};
/******/
/******/ 	// create a fake namespace object
/******/ 	// mode & 1: value is a module id, require it
/******/ 	// mode & 2: merge all properties of value into the ns
/******/ 	// mode & 4: return value when already ns object
/******/ 	// mode & 8|1: behave like require
/******/ 	__webpack_require__.t = function(value, mode) {
/******/ 		if(mode & 1) value = __webpack_require__(value);
/******/ 		if(mode & 8) return value;
/******/ 		if((mode & 4) && typeof value === 'object' && value && value.__esModule) return value;
/******/ 		var ns = Object.create(null);
/******/ 		__webpack_require__.r(ns);
/******/ 		Object.defineProperty(ns, 'default', { enumerable: true, value: value });
/******/ 		if(mode & 2 && typeof value != 'string') for(var key in value) __webpack_require__.d(ns, key, function(key) { return value[key]; }.bind(null, key));
/******/ 		return ns;
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "js/";
/******/
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = "./Battleship2.MVC/wwwroot/js/dist/reactgamehub.jsx");
/******/ })
/************************************************************************/
/******/ ({

/***/ "./Battleship2.MVC/wwwroot/js/dist/reactgamehub.jsx":
/*!**********************************************************!*\
  !*** ./Battleship2.MVC/wwwroot/js/dist/reactgamehub.jsx ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

eval("function _toConsumableArray(arr) { return _arrayWithoutHoles(arr) || _iterableToArray(arr) || _nonIterableSpread(); }\n\nfunction _nonIterableSpread() { throw new TypeError(\"Invalid attempt to spread non-iterable instance\"); }\n\nfunction _iterableToArray(iter) { if (Symbol.iterator in Object(iter) || Object.prototype.toString.call(iter) === \"[object Arguments]\") return Array.from(iter); }\n\nfunction _arrayWithoutHoles(arr) { if (Array.isArray(arr)) { for (var i = 0, arr2 = new Array(arr.length); i < arr.length; i++) { arr2[i] = arr[i]; } return arr2; } }\n\nfunction _typeof(obj) { if (typeof Symbol === \"function\" && typeof Symbol.iterator === \"symbol\") { _typeof = function _typeof(obj) { return typeof obj; }; } else { _typeof = function _typeof(obj) { return obj && typeof Symbol === \"function\" && obj.constructor === Symbol && obj !== Symbol.prototype ? \"symbol\" : typeof obj; }; } return _typeof(obj); }\n\nfunction _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError(\"Cannot call a class as a function\"); } }\n\nfunction _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if (\"value\" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }\n\nfunction _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }\n\nfunction _possibleConstructorReturn(self, call) { if (call && (_typeof(call) === \"object\" || typeof call === \"function\")) { return call; } return _assertThisInitialized(self); }\n\nfunction _assertThisInitialized(self) { if (self === void 0) { throw new ReferenceError(\"this hasn't been initialised - super() hasn't been called\"); } return self; }\n\nfunction _getPrototypeOf(o) { _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf(o) { return o.__proto__ || Object.getPrototypeOf(o); }; return _getPrototypeOf(o); }\n\nfunction _inherits(subClass, superClass) { if (typeof superClass !== \"function\" && superClass !== null) { throw new TypeError(\"Super expression must either be null or a function\"); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, writable: true, configurable: true } }); if (superClass) _setPrototypeOf(subClass, superClass); }\n\nfunction _setPrototypeOf(o, p) { _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf(o, p) { o.__proto__ = p; return o; }; return _setPrototypeOf(o, p); }\n\nvar contextGameId = React.createContext(window.razorGameId);\nvar contextPlayerId = React.createContext(window.razorPlayerId);\nvar contextIsGameCreated = React.createContext(window.razorGameIsCreated);\nvar hubConnection = new signalR.HubConnectionBuilder().withUrl(\"/testhub\").build();\nhubConnection.start().then(function () {\n  return console.log('Connection started!');\n})[\"catch\"](function (err) {\n  return console.log('Error while establishing connection :(');\n});\n\nvar Game =\n/*#__PURE__*/\nfunction (_React$Component) {\n  _inherits(Game, _React$Component);\n\n  function Game() {\n    var _this;\n\n    _classCallCheck(this, Game);\n\n    console.log('game ctor called');\n    _this = _possibleConstructorReturn(this, _getPrototypeOf(Game).call(this));\n    hubConnection.on(\"GameStart\", function () {\n      alert(\"Game has started\");\n\n      _this.setState({\n        showsetup: false\n      });\n    });\n    hubConnection.on(\"OpponentConnected\", function (opponentsName) {\n      alert(\"Your opponent is connected\");\n\n      _this.setState({\n        opponentsName: opponentsName\n      });\n    });\n    hubConnection.on(\"SetId\", function (gameId) {\n      _this.setState({\n        gameId: gameId\n      });\n    });\n    hubConnection.on(\"GameEnded\", function (youarewinner) {\n      if (youarewinner == true) {\n        confirm(\"You've Won!\");\n      } else {\n        confirm(\"You've Lost\");\n      }\n\n      window.location.href = '/Game/Win';\n    });\n    hubConnection.on(\"OpponentLeft\", function () {\n      alert(\"Your opponent left\");\n      this.setState({\n        opponentsName: \"\"\n      });\n    });\n    _this.state = {\n      gameId: _this.contextGameId,\n      playerId: _this.contextPlayerId,\n      isGameCreated: _this.contextIsGameCreated,\n      opponentsName: \"\",\n      unaddedShips: [[1, 4], [2, 3], [3, 2], [4, 1]],\n      showsetup: true\n    };\n    console.log('game ctor finished calling');\n    return _this;\n  }\n\n  _createClass(Game, [{\n    key: \"render\",\n    value: function render() {\n      console.log('game render called');\n      setup = showsetup ? React.createElement(SetUp, null) : \"\";\n      return React.createElement(React.Fragment, null, React.createElement(\"h3\", null, \"Your opponent's Name: \", this.state.opponentsName), React.createElement(\"h3\", null, \"Your game's Id: \", this.state.gameId), React.createElement(Board, null), React.createElement(SetUp, {\n        unaddedShipList: this.state.unaddedShips,\n        hubconnection: this.state.hubConnection\n      }));\n    }\n  }]);\n\n  return Game;\n}(React.Component);\n\nvar Board =\n/*#__PURE__*/\nfunction (_React$Component2) {\n  _inherits(Board, _React$Component2);\n\n  function Board() {\n    var _this2;\n\n    _classCallCheck(this, Board);\n\n    _this2 = _possibleConstructorReturn(this, _getPrototypeOf(Board).call(this));\n    _this2.state = {\n      opponentCells: new Array(10).fill(new Array(10).fill({\n        damaged: false,\n        ship: false,\n        hidden: true\n      })),\n      playerCells: new Array(10).fill(new Array(10).fill({\n        damaged: false,\n        ship: false,\n        hidden: false\n      }))\n    };\n    return _this2;\n  }\n\n  _createClass(Board, [{\n    key: \"render\",\n    value: function render() {\n      return React.createElement(\"table\", null, React.createElement(\"tbody\", null, React.createElement(\"tr\", null, React.createElement(\"td\", null, React.createElement(YourMap, {\n        cells: this.state.playerCells\n      })), React.createElement(\"td\", null, React.createElement(OpponentsMap, {\n        cells: this.state.opponentCells\n      })), React.createElement(\"td\", null, React.createElement(GameActionList, null)))));\n    }\n  }]);\n\n  return Board;\n}(React.Component);\n\nvar OpponentsMap =\n/*#__PURE__*/\nfunction (_React$Component3) {\n  _inherits(OpponentsMap, _React$Component3);\n\n  function OpponentsMap() {\n    var _this3;\n\n    _classCallCheck(this, OpponentsMap);\n\n    _this3 = _possibleConstructorReturn(this, _getPrototypeOf(OpponentsMap).call(this));\n    _this3.state = {\n      shootingLocked: true,\n      youAreActivePlayer: false\n    };\n    hubConnection.on(\"GameStart\", function () {\n      this.setState({\n        shootingLocked: false\n      });\n    });\n    hubConnection.on(\"YourTurn\", function () {\n      alert(\"Your Turn!\");\n      this.setState({\n        youAreActivePlayer: true\n      });\n    });\n    return _this3;\n  }\n\n  _createClass(OpponentsMap, [{\n    key: \"render\",\n    value: function render() {\n      return React.createElement(React.Fragment, null, React.createElement(\"h3\", null, \"Your Opponent's Map\"), React.createElement(ShipMap, {\n        cells: this.props.cells,\n        owner: \"opponent\"\n      }));\n    }\n  }]);\n\n  return OpponentsMap;\n}(React.Component);\n\nvar YourMap =\n/*#__PURE__*/\nfunction (_React$Component4) {\n  _inherits(YourMap, _React$Component4);\n\n  function YourMap() {\n    var _this4;\n\n    _classCallCheck(this, YourMap);\n\n    _this4 = _possibleConstructorReturn(this, _getPrototypeOf(YourMap).call(this));\n    _this4.state = {\n      shipAdditionLocked: true,\n      shipsHead: null\n    };\n    return _this4;\n  }\n\n  _createClass(YourMap, [{\n    key: \"render\",\n    value: function render() {\n      return React.createElement(React.Fragment, null, React.createElement(\"h3\", null, \"Your Map\"), React.createElement(ShipMap, {\n        cells: this.props.cells,\n        owner: \"player\"\n      }));\n    }\n  }]);\n\n  return YourMap;\n}(React.Component);\n\nvar ShipMap =\n/*#__PURE__*/\nfunction (_React$Component5) {\n  _inherits(ShipMap, _React$Component5);\n\n  function ShipMap(props) {\n    var _this5;\n\n    _classCallCheck(this, ShipMap);\n\n    hubConnection.on(\"YourFieldShooted\", function (shootedJsonData, shotdata) {\n      if (this.props.owner == \"player\") {\n        var shootedData = JSON.parse(JSON.stringify(eval(\"(\" + shootedJsonData + \")\")));\n\n        var shootedCells = _toConsumableArray(this.state.cells);\n\n        shootedData.forEach(function (shootedcoord) {\n          shootedCells[shootedcoord.Item1.coordX][shootedcoord.Item1.coordY].damaged = true;\n        });\n        this.setState({\n          cells: shootedCells\n        });\n      }\n    });\n    _this5.state = {\n      cells: props.cells\n    };\n    return _possibleConstructorReturn(_this5);\n  }\n\n  _createClass(ShipMap, [{\n    key: \"render\",\n    value: function render() {\n      var rowlist = [];\n\n      for (var y = 10; y > 0; y--) {\n        rowlist.push(this.renderRow(y));\n      }\n\n      return React.createElement(\"table\", null, React.createElement(\"tbody\", null, rowlist));\n    }\n  }, {\n    key: \"renderRow\",\n    value: function renderRow(y) {\n      var cellList = [];\n\n      for (var x = 1; x < 11; x++) {\n        cellList.push(React.createElement(\"td\", null, React.createElement(MapCell, {\n          cellbag: this.state.cells[10 - y][x - 1],\n          keyitem: (x, y)\n        })));\n      }\n\n      return React.createElement(\"tr\", {\n        key: y\n      }, cellList);\n    }\n  }]);\n\n  return ShipMap;\n}(React.Component);\n\nvar MapCell =\n/*#__PURE__*/\nfunction (_React$Component6) {\n  _inherits(MapCell, _React$Component6);\n\n  function MapCell() {\n    _classCallCheck(this, MapCell);\n\n    return _possibleConstructorReturn(this, _getPrototypeOf(MapCell).apply(this, arguments));\n  }\n\n  _createClass(MapCell, [{\n    key: \"render\",\n    value: function render() {\n      var cellclass = this.props.cellbag.ship ? \"ship\" : this.props.cellbag.hidden ? \"hiddencell\" : \"noship\";\n      var damaged = this.props.cellbag.damaged ? React.createElement(\"div\", {\n        \"class\": \"hittedfield\"\n      }) : \"\";\n      return React.createElement(\"div\", {\n        className: \"cell \" + cellclass,\n        key: this.props.keyitem\n      }, damaged);\n    }\n  }]);\n\n  return MapCell;\n}(React.Component);\n\nvar GameActionList =\n/*#__PURE__*/\nfunction (_React$Component7) {\n  _inherits(GameActionList, _React$Component7);\n\n  function GameActionList() {\n    var _this6;\n\n    _classCallCheck(this, GameActionList);\n\n    hubConnection.on(\"YourFieldShooted\", function (shootedJsonData, shotdata) {\n      this.setState({\n        messages: _toConsumableArray(messages).push(shotdata)\n      });\n    });\n    _this6.state = {\n      messages: []\n    };\n    return _possibleConstructorReturn(_this6);\n  }\n\n  _createClass(GameActionList, [{\n    key: \"render\",\n    value: function render() {\n      var gameActionsListItems = this.props.gameActions.map(function (actionString) {\n        return React.createElement(\"li\", {\n          className: \"list-group-item\"\n        }, actionString);\n      });\n      return React.createElement(\"ul\", {\n        className: \"list-group\",\n        id: \"shotlist\"\n      }, React.createElement(\"li\", {\n        className: \"list-group-item list-group-item-primary\"\n      }, \"Game actions are displayed here\"), gameActionsListItems);\n    }\n  }]);\n\n  return GameActionList;\n}(React.Component);\n\nvar SetUp =\n/*#__PURE__*/\nfunction (_React$Component8) {\n  _inherits(SetUp, _React$Component8);\n\n  function SetUp() {\n    _classCallCheck(this, SetUp);\n\n    return _possibleConstructorReturn(this, _getPrototypeOf(SetUp).apply(this, arguments));\n  }\n\n  _createClass(SetUp, [{\n    key: \"render\",\n    value: function render() {\n      return React.createElement(React.Fragment, null, React.createElement(\"h3\", null, \"Your Unadded Ships\"), React.createElement(UnaddedShipList, {\n        unaddedShipList: this.props.unaddedShipList\n      }), React.createElement(\"button\", {\n        type: \"button\",\n        className: \"btn btn-primary btn-lg\"\n      }, \"Ready\"));\n    }\n  }]);\n\n  return SetUp;\n}(React.Component);\n\nvar UnaddedShipList =\n/*#__PURE__*/\nfunction (_React$Component9) {\n  _inherits(UnaddedShipList, _React$Component9);\n\n  function UnaddedShipList() {\n    _classCallCheck(this, UnaddedShipList);\n\n    return _possibleConstructorReturn(this, _getPrototypeOf(UnaddedShipList).apply(this, arguments));\n  }\n\n  _createClass(UnaddedShipList, [{\n    key: \"render\",\n    value: function render() {\n      var unaddedShipList = this.props.unaddedShipList.map(function (item) {\n        var shipOfCurrentTypeList = [];\n\n        for (var i = 0; i < item[1]; i++) {\n          shipOfCurrentTypeList.push(React.createElement(UnaddedShip, {\n            decks: item[0]\n          }));\n        }\n\n        return shipOfCurrentTypeList;\n      });\n      return React.createElement(\"table\", null, React.createElement(\"tbody\", null, React.createElement(\"tr\", null, unaddedShipList)));\n    }\n  }]);\n\n  return UnaddedShipList;\n}(React.Component);\n\nvar UnaddedShip =\n/*#__PURE__*/\nfunction (_React$Component10) {\n  _inherits(UnaddedShip, _React$Component10);\n\n  function UnaddedShip() {\n    _classCallCheck(this, UnaddedShip);\n\n    return _possibleConstructorReturn(this, _getPrototypeOf(UnaddedShip).apply(this, arguments));\n  }\n\n  _createClass(UnaddedShip, [{\n    key: \"renderShipDecks\",\n    value: function renderShipDecks() {\n      var decks = [];\n\n      for (var i = 0; i < this.props.decks; i++) {\n        var cellbag = {\n          ship: true,\n          damaged: false,\n          hidden: false\n        };\n        decks.push(React.createElement(MapCell, {\n          cellbag: cellbag\n        }));\n      }\n\n      return decks;\n    }\n  }, {\n    key: \"render\",\n    value: function render() {\n      return React.createElement(\"td\", {\n        className: \"shipexamplecell\"\n      }, React.createElement(\"div\", {\n        className: \"shipexample\"\n      }, this.renderShipDecks()));\n    }\n  }]);\n\n  return UnaddedShip;\n}(React.Component);\n\nReactDOM.render(React.createElement(Game, null), document.getElementById('content'));\n\n//# sourceURL=webpack:///./Battleship2.MVC/wwwroot/js/dist/reactgamehub.jsx?");

/***/ })

/******/ });