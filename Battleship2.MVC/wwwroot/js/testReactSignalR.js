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
/******/ 	return __webpack_require__(__webpack_require__.s = "./Battleship2.MVC/wwwroot/js/dist/testReactSignalR.jsx");
/******/ })
/************************************************************************/
/******/ ({

/***/ "./Battleship2.MVC/wwwroot/js/dist/testReactSignalR.jsx":
/*!**************************************************************!*\
  !*** ./Battleship2.MVC/wwwroot/js/dist/testReactSignalR.jsx ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

eval("function _typeof(obj) { if (typeof Symbol === \"function\" && typeof Symbol.iterator === \"symbol\") { _typeof = function _typeof(obj) { return typeof obj; }; } else { _typeof = function _typeof(obj) { return obj && typeof Symbol === \"function\" && obj.constructor === Symbol && obj !== Symbol.prototype ? \"symbol\" : typeof obj; }; } return _typeof(obj); }\n\nfunction _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError(\"Cannot call a class as a function\"); } }\n\nfunction _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if (\"value\" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }\n\nfunction _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }\n\nfunction _possibleConstructorReturn(self, call) { if (call && (_typeof(call) === \"object\" || typeof call === \"function\")) { return call; } return _assertThisInitialized(self); }\n\nfunction _getPrototypeOf(o) { _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf(o) { return o.__proto__ || Object.getPrototypeOf(o); }; return _getPrototypeOf(o); }\n\nfunction _assertThisInitialized(self) { if (self === void 0) { throw new ReferenceError(\"this hasn't been initialised - super() hasn't been called\"); } return self; }\n\nfunction _inherits(subClass, superClass) { if (typeof superClass !== \"function\" && superClass !== null) { throw new TypeError(\"Super expression must either be null or a function\"); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, writable: true, configurable: true } }); if (superClass) _setPrototypeOf(subClass, superClass); }\n\nfunction _setPrototypeOf(o, p) { _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf(o, p) { o.__proto__ = p; return o; }; return _setPrototypeOf(o, p); }\n\nfunction _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }\n\nvar CommentBox =\n/*#__PURE__*/\nfunction (_React$Component) {\n  _inherits(CommentBox, _React$Component);\n\n  function CommentBox(props) {\n    var _this;\n\n    _classCallCheck(this, CommentBox);\n\n    _this = _possibleConstructorReturn(this, _getPrototypeOf(CommentBox).call(this, props));\n\n    _defineProperty(_assertThisInitialized(_this), \"componentDidMount\", function () {});\n\n    var hubConnection = new signalR.HubConnectionBuilder().withUrl(\"/testhub\").build();\n    hubConnection.start().then(function () {\n      return console.log('Connection started!');\n    })[\"catch\"](function (err) {\n      return console.log('Error while establishing connection :(');\n    });\n    _this.state = {\n      message: 'Click me!',\n      hubConnection: hubConnection\n    };\n\n    _this.state.hubConnection.on('message', function (receivedMessage) {\n      console.log(\"commentbox event message invoked\");\n    });\n\n    return _this;\n  }\n\n  _createClass(CommentBox, [{\n    key: \"render\",\n    value: function render() {\n      return React.createElement(CommentItem, {\n        hubConnection: this.state.hubConnection\n      });\n    }\n  }]);\n\n  return CommentBox;\n}(React.Component);\n\nvar CommentItem =\n/*#__PURE__*/\nfunction (_React$Component2) {\n  _inherits(CommentItem, _React$Component2);\n\n  function CommentItem(props) {\n    var _this2;\n\n    _classCallCheck(this, CommentItem);\n\n    _this2 = _possibleConstructorReturn(this, _getPrototypeOf(CommentItem).call(this, props));\n    _this2.state = {\n      message: 'I am Comment Item!',\n      hubConnection: props.hubConnection\n    };\n\n    _this2.state.hubConnection.on('message', function (receivedMessage) {\n      console.log(\"commentitems event message invoked\");\n\n      _this2.setState({\n        message: receivedMessage\n      });\n    });\n\n    console.log(\"commentitem constructor called\");\n    return _this2;\n  }\n\n  _createClass(CommentItem, [{\n    key: \"render\",\n    value: function render() {\n      var _this3 = this;\n\n      return React.createElement(\"button\", {\n        className: \"commentItem\",\n        onClick: function onClick() {\n          _this3.state.hubConnection.invoke(\"Ready\");\n        }\n      }, \" \", this.state.message, \" \");\n    }\n  }]);\n\n  return CommentItem;\n}(React.Component);\n\nReactDOM.render(React.createElement(CommentBox, null), document.getElementById('content'));\n\n//# sourceURL=webpack:///./Battleship2.MVC/wwwroot/js/dist/testReactSignalR.jsx?");

/***/ })

/******/ });